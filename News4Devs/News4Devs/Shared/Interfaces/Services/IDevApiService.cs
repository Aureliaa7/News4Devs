using News4Devs.Core.DTOs;
using News4Devs.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IDevApiService
    {
        /// <summary>
        /// Makes an Http Get request to Dev.to API in order to get the articles
        /// </summary>
        /// <param name="devApiQueryParamsModel">A model containing
        /// all the query params that can be used when calling Dev API</param>
        /// <returns>A list of articles</returns>
        Task<IList<ExtendedArticleDto>> GetArticlesAsync(DevApiQueryParamsModel devApiQueryParamsModel);
    }
}
