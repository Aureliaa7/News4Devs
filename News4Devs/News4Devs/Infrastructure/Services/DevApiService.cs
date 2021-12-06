using News4Devs.Infrastructure.Helpers;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class DevApiService : IDevApiService
    {
        private readonly string devAPIBaseUrl = "https://dev.to/api/articles";

        private readonly IApiService apiService;

        public DevApiService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IList<ExtendedArticleDto>> GetArticlesAsync(DevApiQueryParamsModel devApiQueryParamsModel)
        {
            var queryParams = QueryParamsHelper.GetQueryParams(devApiQueryParamsModel);

            var articlesDtos = await apiService.GetAsync<IList<ArticleDto>>(
                devAPIBaseUrl,
                queryParams);

            var extendedArticlesDtos = new List<ExtendedArticleDto>();
            if (articlesDtos?.Any() == true)
            {
                articlesDtos
                    .Where(x => !string.IsNullOrEmpty(x.tags))
                    .ToList()
                    .ForEach(x => extendedArticlesDtos.Add(new ExtendedArticleDto { Article = x }));
            }

            return extendedArticlesDtos;
        }
    }
}
