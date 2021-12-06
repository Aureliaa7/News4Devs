using News4Devs.Infrastructure.Helpers;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class QuotableService : IQuotableService
    {
        private readonly string quotableAPIUrl = "https://api.quotable.io";

        private readonly IApiService apiService;

        public QuotableService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<QuotableApiResponseDto> GetRandomQuoteAsync(QuotableApiQueryParamsModel queryParamsModel)
        {
            var queryParams = QueryParamsHelper.GetQueryParams(queryParamsModel);

            return await apiService.GetAsync<QuotableApiResponseDto>($"{quotableAPIUrl}/random", queryParams);
        }
    }
}
