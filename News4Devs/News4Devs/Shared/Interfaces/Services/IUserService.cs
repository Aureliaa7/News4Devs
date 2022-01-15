using News4Devs.Shared.Entities;
using News4Devs.Shared.Models;
using System;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid id);

        Task<PagedResponseModel<User>> GetAllAsync(PaginationFilter paginationFilter);
    }
}
