using Blazored.LocalStorage;
using News4Devs.Client.Models;
using News4Devs.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace News4Devs.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public HttpClientService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string apiEndpoint)
        {
            string token = await localStorageService.GetItemAsStringAsync(ClientConstants.Token);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await HandleHttpRequestAsync<T>(requestMessage);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string apiEndpoint, ByteArrayContent content)
        {
            string token = await localStorageService.GetItemAsStringAsync(ClientConstants.Token);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = content;

            return await HandleHttpRequestAsync<T>(requestMessage);
        }

        private async Task<ApiResponse<T>> HandleHttpRequestAsync<T>(HttpRequestMessage requestMessage)
        {
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            var result = new ApiResponse<T>
            {
                Data = JsonConvert.DeserializeObject<T>(content),
                StatusCode = response.StatusCode
            };

            return result;
        }
    }
}
