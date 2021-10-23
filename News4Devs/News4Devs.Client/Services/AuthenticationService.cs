using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using News4Devs.Client.Helpers;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core;
using News4Devs.Core.DTOs;
using News4Devs.Core.Models;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientService httpClientService;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authStateProvider;

        public AuthenticationService(
            IHttpClientService httpClientService, 
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authStateProvider)
        {
            this.httpClientService = httpClientService;
            this.localStorageService = localStorageService;
            this.authStateProvider = authStateProvider;
        }

        public async Task<string> LoginAsync(LoginDto loginModel)
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(loginModel);
            var result = await httpClientService.PostAsync<JwtToken>("accounts/login", byteArrayContent);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var token = result.Data.Token;
                await localStorageService.SetItemAsStringAsync(ClientConstants.Token, token);
                ((News4DevsAuthenticationStateProvider)authStateProvider).NotifyUserAuthentication(token);
                string userId = JwtHelper.GetClaimValueByName(token, Constants.UserId);
                
                return userId;
            }

            return null;
        }

        public async Task LogoutAsync()
        {
            await localStorageService.RemoveItemAsync(ClientConstants.Token);
            ((News4DevsAuthenticationStateProvider)authStateProvider).NotifyUserLogout();
        }
    }
}
