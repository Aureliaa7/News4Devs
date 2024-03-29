﻿using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Exceptions;
using News4Devs.Shared.Helpers;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Interfaces.UnitOfWork;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;
        private readonly IImageService imageService;

        public AccountService(
            IUnitOfWork unitOfWork,
            IJwtService jwtService,
            IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.jwtService = jwtService;
            this.imageService = imageService;
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

            return token;
        }

        public async Task<User> RegisterAsync(User user, byte[] profilePhotoContent = null)
        {
            bool userExists = await unitOfWork.UsersRepository.ExistsAsync(u => u.Email.Equals(user.Email));
            if (userExists)
            {
                throw new DuplicateEmailException("A user with the same email already exists!");
            }

            PasswordHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            if (profilePhotoContent != null)
            {
                user.ProfilePhotoName = await imageService.SaveImageAsync(profilePhotoContent);
            }

            var newUser = await unitOfWork.UsersRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            return newUser;
        }
    }
}
