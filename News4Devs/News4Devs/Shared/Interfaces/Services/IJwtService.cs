using News4Devs.Shared.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token
        /// </summary>
        /// <param name="loginDto">The user's credentials</param>
        /// <returns>A JWT token or an empty string</returns>
        Task<string> GenerateTokenAsync(LoginDto loginDto);
    }
}
