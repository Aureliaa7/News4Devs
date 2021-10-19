using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Helpers;
using News4Devs.Core.DTOs;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace News4Devs.Client.Pages.Accounts
{
    public partial class Login : ComponentBase
    {
        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private LoginDto loginModel = new();

        private async Task LogIn()
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(loginModel);
            var result = await HttpClient.PostAsync("accounts/login", byteArrayContent);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var token = await HttpResponseMessageHelper.DeserializeContentAsync<JwtToken>(result);
                //TODO store the JWT token and redirect the user to his/her profile page
                NavigationManager.NavigateTo("/profile");
            }
            else
            {
                ToastService.ShowError("Incorrect credentials!");
            }
            loginModel = new();
        }

        private class JwtToken
        {
            public string Token { get; set; }
        }
    }
}
