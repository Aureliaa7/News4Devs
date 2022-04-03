using News4Devs.Shared.DTOs;
using News4Devs.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface INewsApiService
    {
        Task<IList<ExtendedArticleDto>> GetArticlesAsync(NewsApiQueryParamsModel queryParamsModel);
    }
}
