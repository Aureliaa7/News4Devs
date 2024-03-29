﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class UsersController : News4DevsController
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IAccountService accountService, IMapper mapper, IUserService userService)
        {
            this.accountService = accountService;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var createdUser = await accountService.RegisterAsync(mapper.Map<User>(registerDto), registerDto.ProfilePhotoContent);

            /*Note: Can't use nameof(actionName) because ASP.NET Core MVC trims the suffix Async
             * from action names by default.
             See: https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnetcore#mvc-async-suffix-trimmed-from-controller-action-names*/
            return CreatedAtAction("GetAccountInfo", 
                new { id = createdUser.Id }, mapper.Map<UserDto>(createdUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            /*Note: No need to check if the model is valid since the controller is decorated with [ApiController] attribute
            which makes model validation errors automatically trigger an HTTP 400 response */

            var jwtToken = await accountService.LoginAsync(loginDto);
            if (!string.IsNullOrEmpty(jwtToken))
            {
                return Ok(new JwtToken { Token = jwtToken });
            }

            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountInfoAsync([FromRoute] Guid id)
        {
            var user = await userService.GetByIdAsync(id);
            return Ok(mapper.Map<UserDto>(user));
        }

        [HttpGet("exclude={userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllAccounts([FromRoute] Guid userId, [FromQuery] PaginationFilter paginationFilter)
        {
            var users = await userService.GetAllAsync(paginationFilter, userId);
            var result = mapper.Map<PagedResponseDto<UserDto>>(users);

            return Ok(result);
        }

        [HttpGet("contacts/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetContacts([FromRoute] Guid userId, [FromQuery] PaginationFilter paginationFilter)
        {
            var users = await userService.GetContactsAsync(userId, paginationFilter);
            var result = mapper.Map<PagedResponseDto<UserDto>>(users);

            return Ok(result);
        }
    }
}
