using News4Devs.Core;
using News4Devs.Core.DTOs;
using News4Devs.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class QuotableService : IQuotableService
    {
        private readonly IApiService apiService;

        public QuotableService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<QuotableApiResponseDto> GetRandomQuoteAsync()
        {
            return await apiService.GetAsync<QuotableApiResponseDto>($"{Constants.QuotableAPIUrl}/random");
        }
    }
}
