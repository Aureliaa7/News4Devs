using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Accounts
{
    public partial class Login
    {
        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        private LoginDto loginModel = new();

        private async Task LogIn()
        {
            string userId = await AuthenticationService.LoginAsync(loginModel);
            if (string.IsNullOrEmpty(userId))
            {
                ToastService.ShowError("Incorrect credentials!");
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }

            loginModel = new();
        }
    }
}
