using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Helpers;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core;
using News4Devs.Core.DTOs;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Pages.Accounts
{
    public partial class Login
    {
        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private LoginDto loginModel = new();

        private async Task LogIn()
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(loginModel);
            var result = await HttpClientService.PostAsync<JwtToken>("accounts/login", byteArrayContent);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var token = result.Data.Token;
                await LocalStorageService.SetItemAsStringAsync(ClientConstants.Token, token);
                string userId = JwtHelper.GetClaimValueByName(token, Constants.UserId);
                NavigationManager.NavigateTo($"/profile/{userId}");
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
