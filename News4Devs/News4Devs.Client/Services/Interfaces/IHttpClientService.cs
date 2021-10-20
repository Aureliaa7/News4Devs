using News4Devs.Client.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace News4Devs.Client.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<ApiResponse<T>> GetAsync<T>(string apiEndpoint);

        Task<ApiResponse<T>> PostAsync<T>(string apiEndpoint, ByteArrayContent content);
    }
}
