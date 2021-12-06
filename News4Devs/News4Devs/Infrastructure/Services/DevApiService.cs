﻿using News4Devs.Shared.DTOs;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var queryParams = GetQueryParams(devApiQueryParamsModel);

            var articlesDtos = await apiService.GetAsync<IList<ArticleDto>>(
                devAPIBaseUrl,
                queryParams);

            var extendedArticlesDtos = new List<ExtendedArticleDto>();
            if (articlesDtos?.Any() == true)
            {
                articlesDtos.ToList().ForEach(x => extendedArticlesDtos.Add(new ExtendedArticleDto { Article = x }));
            }

            return extendedArticlesDtos;
        }

        private static IDictionary<string, string> GetQueryParams(DevApiQueryParamsModel devApiQueryParamsModel)
        {
            var queryParams = new Dictionary<string, string>();

            foreach (PropertyInfo prop in devApiQueryParamsModel.GetType().GetProperties())
            {
                if (prop.GetValue(devApiQueryParamsModel) != null)
                {
                    queryParams.Add(prop.Name, prop.GetValue(devApiQueryParamsModel).ToString());
                }
            }

            return queryParams;
        }
    }
}
