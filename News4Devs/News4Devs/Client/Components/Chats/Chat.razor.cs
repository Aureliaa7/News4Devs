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
        private HubConnection hubConnection;
        private UserDto toUser;
        private List<ChatMessageDto> chatMessages = new();
        private string currentUserId;
        private string messageText;
        private IJSObjectReference jSObjectReference;
        private int pageNumber = 1;
        private bool isLoading;

        [Inject]
        private IHttpClientService HttpService { get; set; }

        [Inject]
        private IAuthenticationService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            currentUserId = await AuthService.GetCurrentUserIdAsync();
            jSObjectReference = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/helper.js");

            await SetUpSignalR();

            if (Id != null)
            {
                await SetToUserAsync();
                await GetMessagesAsync();
            }

            isLoading = false;
            StateHasChanged();
            await jSObjectReference.InvokeVoidAsync("scrollToBottom");
            await jSObjectReference.InvokeVoidAsync("handleScrollEvent");
        }

        private async Task SetUpSignalR()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            hubConnection.On<ChatMessageDto>("ReceiveMessage", async (message) =>
            {
                chatMessages.Add(message);
                await jSObjectReference.InvokeVoidAsync("scrollToBottom");
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        private async Task SetToUserAsync()
        {
            var response = await HttpService.GetAsync<UserDto>($"{ClientConstants.BaseUrl}/accounts/{Id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                toUser = response.Data;
            }
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

                await hubConnection.SendAsync("SendMessage", message);
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
    }
}
