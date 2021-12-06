using News4Devs.Shared;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Interfaces.Services;
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

        public async Task<QuotableApiResponseDto> GetRandomQuoteAsync()
        {
            return await apiService.GetAsync<QuotableApiResponseDto>($"{quotableAPIUrl}/random");
        }
    }
}
