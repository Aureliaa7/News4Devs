using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using News4Devs.Core;
using News4Devs.Core.DTOs;
using News4Devs.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class NewsCatcherApiService : INewsCatcherApiService
    {
        private readonly HttpClient httpClient;

        public NewsCatcherApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(Constants.NewsCatcherApiBaseUrl);
            string apiKey = configuration.GetSection("x-rapidapi-key").Value;  // maybe extract x-... as a constant
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
        }

        public async Task<NewsCatcherApiResponseDto> GetNewsAsync(IDictionary<string, string> queryParams)
        {
            string url = QueryHelpers.AddQueryString(Constants.NewsCatcherApiBaseUrl, queryParams);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<NewsCatcherApiResponseDto>(content);

            return apiResponse;
        }
    }
}
