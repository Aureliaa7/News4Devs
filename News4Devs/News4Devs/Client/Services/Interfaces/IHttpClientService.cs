﻿using News4Devs.Shared.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace News4Devs.Client.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<ApiResponse<T>> GetAsync<T>(string apiEndpoint);

        Task<ApiResponse<T>> PostAsync<T>(string apiEndpoint, ByteArrayContent content);

        Task<HttpStatusCode> PutAsync(string apiEndpoint, ByteArrayContent content);

        Task<ApiResponse<T>> DeleteAsync<T>(string apiEndpoint, ByteArrayContent content = null);
    }
}
