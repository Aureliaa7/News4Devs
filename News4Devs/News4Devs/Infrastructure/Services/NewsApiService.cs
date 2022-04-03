using Microsoft.Extensions.Configuration;
using News4Devs.Infrastructure.Helpers;
using News4Devs.Shared;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class NewsApiService : INewsApiService
    {
         private readonly string newsAPIBaseUrl = "https://newsapi.org/v2/everything";

        private readonly IApiService apiService;
        private readonly IConfiguration configuration;

        public NewsApiService(IApiService apiService, IConfiguration configuration)
        {
            this.apiService = apiService;
            this.configuration = configuration;
        }

        public async Task<IList<ExtendedArticleDto>> GetArticlesAsync(NewsApiQueryParamsModel queryParamsModel)
        {
            // Note: The API key is a mandatory query param
            queryParamsModel.apiKey = configuration.GetSection(Constants.NewsApiKey).Value;

            var queryParams = QueryParamsHelper.GetQueryParams(queryParamsModel);

            var apiResponse= await apiService.GetAsync<NewsApiResponseDto>(
                newsAPIBaseUrl,
                queryParams);

            var extendedArticlesDtos = new List<ExtendedArticleDto>();
            if (apiResponse.status == "ok")
            {
                apiResponse?.articles
                    .ToList()
                    .ForEach(x => extendedArticlesDtos.Add(new ExtendedArticleDto { Article = x }));
            }

            return extendedArticlesDtos;
        }
    }
}
