using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using News4Devs.Client.Helpers;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Models;
using System.Net;
using System.Security.Claims;
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

        public async Task<string> GetCurrentUserFullNameAsync()
        {
            return await GetClaimByNameAsync(Constants.UserFullName);
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            return await GetClaimByNameAsync(Constants.UserId);
        }

        private async Task<string> GetClaimByNameAsync(string name)
        {
            var token = await localStorageService.GetItemAsStringAsync(ClientConstants.Token);

            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            return JwtHelper.GetClaimValueByName(token, name);
        }

        public async Task<string> LoginAsync(LoginDto loginModel)
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(loginModel);
            var result = await httpClientService.PostAsync<JwtToken>($"{ClientConstants.BaseUrl}/accounts/login", byteArrayContent);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var token = result.Data.Token;
                await localStorageService.SetItemAsStringAsync(ClientConstants.Token, token);
                ((News4DevsAuthenticationStateProvider)authStateProvider).NotifyUserAuthentication(token);

                return JwtHelper.GetClaimValueByName(token, Constants.UserId);
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
