using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using News4Devs.Core;
using News4Devs.Core.DTOs;
using News4Devs.Core.Exceptions;
using News4Devs.Core.Helpers;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Interfaces.UnitOfWork;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public JwtService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(LoginDto loginDto)
        {
            var user = await unitOfWork.UsersRepository.GetFirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                throw new EntityNotFoundException("The user was not found!");
            }

            bool isCorrectPassword = PasswordHelper.IsCorrectPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);

            if (!isCorrectPassword)
            {
                throw new EntityNotFoundException("Wrong credentials!");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            string jwtKey = configuration.GetSection("Authentication:JWTKey").Value;
            var tokenKeyBytes = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Email, loginDto.Email),
                new Claim(ClaimTypes.Name, string.Concat(user.FirstName, " ", user.LastName)),
                new Claim(Constants.UserId, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKeyBytes), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
