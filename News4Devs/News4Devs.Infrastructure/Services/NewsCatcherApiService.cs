using News4Devs.Core;
using News4Devs.Core.DTOs;
using News4Devs.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class NewsCatcherApiService : INewsCatcherApiService
    {
        private readonly IApiService apiService;

        public NewsCatcherApiService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<NewsCatcherApiResponseDto> GetNewsAsync(IDictionary<string, string> queryParams)
        {
            var apiResponse = await apiService.GetAsync<NewsCatcherApiResponseDto>(
                Constants.NewsCatcherApiBaseUrl, 
                Constants.NewsCatcherApiKeyName, 
                queryParams);

            return apiResponse;
        }
    }
}
