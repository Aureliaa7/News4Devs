using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Pagination;
using System;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Generates a JWT token if the credentials are correct
        /// </summary>
        /// <param name="loginDto">A model containing an email and a password</param>
        /// <returns>The generated JWT token or an empty string</returns>
        Task<string> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user">A model containing user info</param>
        /// <param name="profilePhotoContent">The profile photo content</param>
        /// <returns>The created user</returns>
        Task<User> RegisterAsync(User user, byte[] profilePhotoContent = null);

        /// <summary>
        /// Returns the user's details based on their id
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <returns>An object containing the user's account details</returns>
        Task<User> GetByIdAsync(Guid id);

        Task<PagedResponseModel<User>> GetAllAsync(PaginationFilter paginationFilter);
    }
}
