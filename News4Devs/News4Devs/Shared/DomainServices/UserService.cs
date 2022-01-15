using News4Devs.Shared.Entities;
using News4Devs.Shared.Exceptions;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Interfaces.UnitOfWork;
using News4Devs.Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserPaginationService userPaginationService;

        public UserService(IUnitOfWork unitOfWork, IUserPaginationService userPaginationService)
        {
            this.unitOfWork = unitOfWork;
            this.userPaginationService = userPaginationService;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var searchedUser = await unitOfWork.UsersRepository.GetByIdAsync(id);
            if (searchedUser == null)
            {
                throw new EntityNotFoundException($"The user with the id {id} was not found!");
            }

            return searchedUser;
        }

        public async Task<PagedResponseModel<User>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var pagedResponse = await userPaginationService.GetPagedResponseAsync(Constants.UsersAddress, paginationFilter);

            return pagedResponse;
        }

        public async Task<PagedResponseModel<User>> GetContactsAsync(Guid userId, PaginationFilter paginationFilter)
        {
            var usersIds = (await unitOfWork.MessagesRepository
                    .GetAllAsync(x => x.FromUserId == userId && x.ToUserId.HasValue))
                    .GroupBy(x => x.ToUserId)
                    .Select(x => x.Key.Value)
                .Union((await unitOfWork.MessagesRepository
                    .GetAllAsync(x => x.ToUserId == userId && x.FromUserId.HasValue))
                    .GroupBy(x => x.FromUserId)
                    .Select(x => x.Key.Value))
                    .ToList();

            return await userPaginationService.GetPagedResponseAsync(Constants.UsersAddress, paginationFilter, x => usersIds.Contains(x.Id));
        }
    }
}
