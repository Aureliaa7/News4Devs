using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
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
