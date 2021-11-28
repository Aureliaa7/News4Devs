using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Makes a request to API in order to authenticate the user
        /// </summary>
        /// <param name="loginModel">A model containing the user's credentials</param>
        /// <returns>The user id or null for incorrect credentials</returns>
        Task<string> LoginAsync(LoginDto loginModel);

        /// <summary>
        /// Logs out the authenticated user
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        Task<string> GetCurrentUserIdAsync();
    }
}
