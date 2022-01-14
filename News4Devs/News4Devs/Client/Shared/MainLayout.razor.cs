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
            System.Console.WriteLine("initialized mainLayout....");
            currentUserId = await AuthService.GetCurrentUserIdAsync();
          
            await SetUpSignalR();
        }

        private async Task SetUpSignalR()
        {
            System.Console.WriteLine("SetUpSignalR...-- main layout comp");
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            await hubConnection.StartAsync();

            hubConnection.On<string, string, string>("NewMessageNotification", async (message, receiverId, senderId) =>
            {
                System.Console.WriteLine("NewMessageNotification...");

                var response = await HttpService.GetAsync<UserDto>($"{ClientConstants.BaseUrl}/accounts/{senderId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    senderUser = response.Data;
                }

                if (currentUserId == receiverId)
                {
                    ToastService.ShowInfo(message, $"{senderUser.FirstName} {senderUser.LastName}",
                        () =>
                        {
                            // TODO solve: if i open a conversation with x and in the same time I get a msg from y and I try to navigate to 
                            // the conversation with y, only the url changes but I can still see the conversation with x
                            NavigationManager.NavigateTo($"/chat/{senderId}");
                            StateHasChanged();
                        });
                }
            });

            //TODO let the user navigate to a profile page
            //TODO: to be solved:  the user should get the toast with new message, no matter the current component---> SOLVED
        }
    }
}
