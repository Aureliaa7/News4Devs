using News4Devs.Core.DTOs;
using News4Devs.Core.Entities;
using News4Devs.Core.Exceptions;
using News4Devs.Core.Helpers;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Interfaces.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace News4Devs.Core.DomainServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;

        public AccountService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.jwtService = jwtService;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            string token = string.Empty;
            try
            {
                token = await jwtService.GenerateTokenAsync(loginDto);
            }
            catch (EntityNotFoundException)
            {
                // swallow the exception
            }
            catch (Exception) { }

            return token;
        }

        public async Task<User> RegisterAsync(User user)
        {
            bool userExists = await unitOfWork.UsersRepository.ExistsAsync(u => u.Email.Equals(user.Email));
            if (userExists)
            {
                throw new DuplicateEmailException("A user with the same email already exists!");
            }

            PasswordHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var newUser = await unitOfWork.UsersRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            //TODO add a service for storing the users' profile photo

            return newUser;
        }
    }
}
