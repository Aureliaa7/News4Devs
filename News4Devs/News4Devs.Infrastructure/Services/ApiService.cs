using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using News4Devs.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<T> GetAsync<T>(
            string url, 
            string apiKeyName = null, 
            IDictionary<string, string> queryParams = null)
        {
            string modifiedUrl = queryParams == null ? url : 
                QueryHelpers.AddQueryString(url, queryParams);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, modifiedUrl);
         
            if (!string.IsNullOrEmpty(apiKeyName))
            {
                string apiKeyValue = configuration.GetSection(apiKeyName).Value;
                requestMessage.Headers.Add(apiKeyName, apiKeyValue);
            }

            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<T>(content);

            return apiResponse;
        }
    }
}
