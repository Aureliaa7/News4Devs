using News4Devs.Core.DTOs;
using News4Devs.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class ITBookstoreService : IITBookstoreService
    {
        private readonly string ITBookstoreAPIUrl = "https://api.itbook.store/1.0/";

        private readonly IApiService apiService;

        public ITBookstoreService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IList<BookDto>> GetNewBooksAsync()
        {
            var apiResponse = await apiService.GetAsync<ItBookstoreApiResponseDto>(ITBookstoreAPIUrl+"new");
            if (apiResponse != null)
            {
                return apiResponse.books;
            }
            return new List<BookDto>();
        }
    }
}
