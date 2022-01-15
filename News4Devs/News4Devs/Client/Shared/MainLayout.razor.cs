using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Shared
{
    public partial class MainLayout
    {
        private HubConnection hubConnection;
        private UserDto senderUser;
        private string currentUserId;

        [Inject]
        private IAuthenticationService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        IHttpClientService HttpService { get; set; }

        [Inject]
        IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            currentUserId = await AuthService.GetCurrentUserIdAsync();
          
            await SetUpSignalR();
        }

        private async Task SetUpSignalR()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            await hubConnection.StartAsync();

            hubConnection.On<string, string, string>("NewMessageNotification", async (message, receiverId, senderId) =>
            {
                var response = await HttpService.GetAsync<UserDto>($"{ClientConstants.BaseUrl}/users/{senderId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    senderUser = response.Data;
                }

                if (currentUserId == receiverId)
                {
                    ToastService.ShowInfo(message, $"{senderUser.FirstName} {senderUser.LastName}",
                        () =>
                        {
                            // Since calling StateHasChanged() has no effect, force component reloading
                            string newUri = $"https://localhost:44397/chat/{senderId}";
                            if (NavigationManager.Uri != newUri)
                            {
                                NavigationManager.NavigateTo(newUri, true);

                            }
                        });
                }
            });
        }
    }
}
