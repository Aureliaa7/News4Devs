﻿using News4Devs.Core.DTOs;
using News4Devs.Core.Entities;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
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
        /// <returns>The created user</returns>
        Task<User> RegisterAsync(User user);
    }
}
