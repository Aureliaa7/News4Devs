using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Pagination;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public abstract class PaginationServiceBase<T, U> : IPaginationService<T, U> where T: class, new()
    {
        public string GetNextPage(string address, int pageNumber, int pageSize, int noPages)
        {
            bool isLastPage = pageNumber == noPages;

            return !isLastPage && noPages > 0 ? GetPageAddress(address, ++pageNumber, pageSize, noPages) : null;
        }

        public string GetPageAddress(string address, int pageNumber, int pageSize, int noPages)
        {
            if (noPages > 0 && pageNumber > 0 && pageNumber <= noPages)
            {
                return $"{address}?pageNumber={pageNumber}&pageSize={pageSize}";
            }
            return null;
        }

        public abstract Task<PagedResponseModel<T>> GetPagedResponseAsync(
            string address,
            PaginationFilter paginationFilter,
            Expression<Func<U, bool>> filter = null);

        public string GetPreviousPage(string address, int pageNumber, int pageSize, int noPages)
        {
            return pageNumber > 1 && noPages > 0 ? GetPageAddress(address, pageNumber - 1, pageSize, noPages) : null;
        }

        public int GetRoundedTotalPages(int totalRecords, int pageSize)
        {
            var totalPages = ((double)totalRecords / pageSize);
            return Convert.ToInt32(Math.Ceiling(totalPages));
        }
    }
}
