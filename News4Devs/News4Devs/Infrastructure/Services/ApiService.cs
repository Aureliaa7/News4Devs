using Microsoft.AspNetCore.WebUtilities;
using News4Devs.Shared.Interfaces.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(
            string url,
            IDictionary<string, string> queryParams = null)
        {
            string modifiedUrl = queryParams == null ? url :
                QueryHelpers.AddQueryString(url, queryParams);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, modifiedUrl);

            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<T>(content);

            return apiResponse;
        }
    }
}
