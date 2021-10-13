using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News4Devs.Core.DTOs;
using News4Devs.Core.Entities;
using News4Devs.Core.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    public class AccountsController : News4DevsController
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var createdUser = await accountService.RegisterAsync(mapper.Map<User>(registerDto));

            /*Note: Can't use nameof(actionName) because ASP.NET Core MVC trims the suffix Async
             * from action names by default.
             See: https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnetcore#mvc-async-suffix-trimmed-from-controller-action-names*/
            return CreatedAtAction("GetAccountInfo", 
                new { id = createdUser.Id }, mapper.Map<UserDto>(createdUser));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public IActionResult GetAccountInfoAsync([FromRoute] Guid id)
        {
            //TODO to be implemented
            return Ok("hello");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            /*Note: No need to check if the model is valid since the action is decorated with [ApiController] attribute
            which makes model validation errors automatically trigger an HTTP 400 response */

            var jwtToken = await accountService.LoginAsync(loginDto);
            if (!string.IsNullOrEmpty(jwtToken))
            {
                return Ok(new { Token = jwtToken });
            }

            return Unauthorized();
        }
    }
}
