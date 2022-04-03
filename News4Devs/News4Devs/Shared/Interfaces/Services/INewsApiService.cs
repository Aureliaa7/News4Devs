using News4Devs.Shared.Models;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface INewsApiService
    {
        Task<NewsApiResponseModel> GetArticlesAsync(NewsApiQueryParamsModel queryParamsModel);
    }
}
