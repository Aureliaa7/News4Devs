using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface INewsCatcherApiService
    {
        /// <summary>
        /// Makes a request to NewsCatcher API in order to get the news
        /// </summary>
        /// <param name="queryParams">The query params used to call
        ///     the NewsCatcherAPI(for instance, lang=en, page=2)
        ///</param>
        /// <returns>An object containing the page number, page size, total pages and a list of articles</returns>
        Task<NewsCatcherApiResponseDto> GetNewsAsync(IDictionary<string, string> queryParams);
    }
}
