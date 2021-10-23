using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using News4Devs.Client.Helpers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace News4Devs.Client.Services
{
    public class News4DevsAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;

        public News4DevsAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsStringAsync(ClientConstants.Token);
            var claimsPrincipal = GetClaimsPrincipal(token);

            return new AuthenticationState(claimsPrincipal);
        }

        /// <summary>
        /// Needed bc. the component isn't notified automatically 
        /// if the underlying authentication state data changes.
        /// </summary>
        /// <param name="token"></param>
        public void NotifyUserAuthentication(string token)
        {
            var claimsPrincipal = GetClaimsPrincipal(token);
            var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(
                new AuthenticationState(
                    new ClaimsPrincipal(
                        new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        private static ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var claims = JwtHelper.GetClaimsFromJWT(token);
            if (claims.Any())
            {
                var claimsIdentity = new ClaimsIdentity(claims, "jwtAuthType");
                return new ClaimsPrincipal(claimsIdentity);
            }

            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }
}
