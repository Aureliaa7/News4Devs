using News4Devs.Core;
using News4Devs.Core.DTOs;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Services
{
    public class DevApiService : IDevApiService
    {
        private readonly IApiService apiService;

        public DevApiService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IList<ArticleDto>> GetArticlesAsync(DevApiQueryParamsModel devApiQueryParamsModel)
        {
            var queryParams = GetQueryParams(devApiQueryParamsModel);

            var apiResponse = await apiService.GetAsync<IList<ArticleDto>>(
                Constants.DevAPIBaseUrl,
                queryParams);

            return apiResponse;
        }

        private IDictionary<string, string> GetQueryParams(DevApiQueryParamsModel devApiQueryParamsModel)
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
