using Blazored.LocalStorage;
using News4Devs.Client.Helpers;
using News4Devs.Client.Models;
using News4Devs.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
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
            var requestMessage = await CreateHttpRequestMessageAsync(HttpMethod.Get, apiEndpoint);

            return await HandleHttpRequestAsync<T>(requestMessage);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string apiEndpoint, ByteArrayContent content)
        {
            var requestMessage = await CreateHttpRequestMessageAsync(HttpMethod.Post, apiEndpoint);
            requestMessage.Content = content;

            return await HandleHttpRequestAsync<T>(requestMessage);
        }

        public async Task<HttpStatusCode> PutAsync(string apiEndpoint, ByteArrayContent content)
        {
            var requestMessage = await CreateHttpRequestMessageAsync(HttpMethod.Put, apiEndpoint);
            requestMessage.Content = content;
            var response = await httpClient.SendAsync(requestMessage);

            return response.StatusCode;
        }

        private async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(HttpMethod httpMethod, string apiEndpoint)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, apiEndpoint);
            string token = await localStorageService.GetItemAsStringAsync(ClientConstants.Token);

            if (!JwtHelper.IsValidJwt(token))
            {
                await localStorageService.RemoveItemAsync(ClientConstants.Token);
            }
            else
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return requestMessage;
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
