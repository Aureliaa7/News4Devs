using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Makes an Http Get request to a specified URL
        /// </summary>
        /// <typeparam name="T">The api response</typeparam>
        /// <param name="url">The given URL</param>
        /// <param name="queryParams">The query params</param>
        /// <returns>An object of type T</returns>
        Task<T> GetAsync<T>(string url, IDictionary<string, string> queryParams = null);
    }
}
