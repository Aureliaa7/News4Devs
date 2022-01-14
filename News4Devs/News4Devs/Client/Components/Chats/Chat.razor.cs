using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using News4Devs.Client.Helpers;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Chats
{
    public partial class Chat
    {
        private UserDto toUser;
        private List<ChatMessageDto> chatMessages = new();
        private string currentUserId;
        private string messageText;
        private IJSObjectReference jSObjectReference;
        private int pageNumber = 1;
        private bool isLoading;
        private bool showDateTime;
        private long currentHoveredMessageId;

        [CascadingParameter]
        private HubConnection hubConnection { get; set; }

        [Inject]
        private IHttpClientService HttpService { get; set; }

        [Inject]
        private IAuthenticationService AuthService { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            currentUserId = await AuthService.GetCurrentUserIdAsync();
            jSObjectReference = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/helper.js");

            await SetUpSignalR();

            if (Id != null)
            {
                toUser = await GetUserAsync(Id);
                await GetMessagesAsync();
            }

            isLoading = false;
            StateHasChanged();
            await jSObjectReference.InvokeVoidAsync("scrollToBottom");
            await jSObjectReference.InvokeVoidAsync("handleScrollEvent");
        }

        private async Task SetUpSignalR()
        {
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                   .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                   .Build();
            }
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }

            HandleReceivedMessage();
        }

        private void HandleReceivedMessage()
        {
            hubConnection.On<ChatMessageDto>("ReceiveMessage", async (message) =>
            {
                // update the messages list for both sender and receiver
                Console.WriteLine("on receiveMessage...");
                if ((message.FromUserId.ToString() == Id && currentUserId == message.ToUserId.ToString()) ||
                 (message.FromUserId.ToString() == currentUserId && message.ToUserId.ToString() == Id))
                {
                    chatMessages.Add(message);
                    StateHasChanged();
                    await jSObjectReference.InvokeVoidAsync("scrollToBottom");
                }
            });
        }

        private async Task<UserDto> GetUserAsync(string id)
        {
            var response = await HttpService.GetAsync<UserDto>($"{ClientConstants.BaseUrl}/accounts/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }

            return new UserDto();
        }

        private async Task GetMessagesAsync()
        {
            var response = await HttpService.GetAsync<PagedResponseDto<ChatMessageDto>>(
                $"{ClientConstants.BaseUrl}/chat/{currentUserId}/{Id}?pageNumber={pageNumber}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                chatMessages.InsertRange(0, response.Data.Data);
                pageNumber++;
            }
        }

        private async Task Send()
        {
            if (hubConnection != null && !string.IsNullOrEmpty(messageText))
            {
                var message = new ChatMessageDto
                {
                    CreatedDate = DateTime.Now,
                    FromUserId = new Guid(currentUserId),
                    ToUserId = new Guid(Id),
                    Message = messageText
                };

                await hubConnection.SendAsync("SendMessageAsync", message);
                if (currentUserId == message.FromUserId.ToString())
                {
                    await hubConnection.SendAsync("NotifyAsync", message.Message, Id, currentUserId);
                }

                messageText = string.Empty;

                var requestContent = ByteArrayContentHelper.ConvertToByteArrayContent(message);
                var result = await HttpService.PostAsync<ChatMessageDto>($"{ClientConstants.BaseUrl}/chat/save", requestContent);
                // if the message was sent, scroll to bottom
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await jSObjectReference.InvokeVoidAsync("scrollToBottom");
                }
            }
        }

        public bool IsConnected =>
            hubConnection?.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }

        private async Task OnKeyDown(KeyboardEventArgs eventArgs)
        {
            if (eventArgs.Code == "Enter")
            {
                messageText = await jSObjectReference.InvokeAsync<string>("getMessage");
                await Send();
            }
        }

        private async Task OnScroll()
        {
            bool hasScrolledToTop = await jSObjectReference.InvokeAsync<bool>("hasScrolledToTop");
            if (hasScrolledToTop)
            {
                await GetMessagesAsync();
            }
        }

        private void OnMouseOver(long messageId)
        {
            showDateTime = true;
            currentHoveredMessageId = messageId;
        }

        private void OnMouseOut()
        {
            showDateTime = false;
            currentHoveredMessageId = 0;
        }
    }
}
