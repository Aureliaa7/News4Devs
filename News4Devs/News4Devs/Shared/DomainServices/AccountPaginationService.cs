using News4Devs.Shared.Entities;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Interfaces.UnitOfWork;
using News4Devs.Shared.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public class AccountPaginationService : PaginationServiceBase<User, User>, IAccountPaginationService
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountPaginationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async override Task<PagedResponseModel<User>> GetPagedResponseAsync(string address, PaginationFilter paginationFilter, Expression<Func<User, bool>> filter = null)
        {
            int totalRecords = await unitOfWork.UsersRepository.GetTotalRecordsAsync(filter);

            var users = (await unitOfWork.UsersRepository.GetAllAsync(filter,
                skip: (paginationFilter.PageNumber - 1) * paginationFilter.PageSize,
                take: paginationFilter.PageSize))
                .ToList();

            var pagedResponse = GetPagedResponseModel(users, address, totalRecords, paginationFilter);
            return pagedResponse;
        }
    }
}
