using News4Devs.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IPaginationService<T, U> where T: class, new()
    {
        Task<PagedResponseModel<T>> GetPagedResponseAsync(
            string address,
            PaginationFilter paginationFilter,
            Expression<Func<U, bool>> filter = null);

        string GetPreviousPage(string address, int pageNumber, int pageSize, int noPages);

        string GetNextPage(string address, int pageNumber, int pageSize, int noPages);

        string GetPageAddress(string address, int pageNumber, int pageSize, int noPages);

        int GetRoundedTotalPages(int totalRecords, int pageSize);

        PagedResponseModel<T> GetPagedResponseModel(IList<T> data, string address, int totalRecords, PaginationFilter paginationFilter);
    }
}
