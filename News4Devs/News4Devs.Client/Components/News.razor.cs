using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class News
    {
        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private List<ArticleDto> articles = new List<ArticleDto>();
        private int pageNumber = 1;

        protected override async Task OnInitializedAsync()
        {
            await GetArticlesAsync();
        }

        private async Task LoadMoreArticles()
        {
            await GetArticlesAsync();
        }

        private string GetUrl()
        {
            return $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}&state=fresh";
        }

        private async Task GetArticlesAsync()
        {
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<IList<ArticleDto>>(url);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ToastService.ShowError("An unexpected error occured during retrieving the news");
            }
            else
            {
                var devApiResponse = response.Data;
                if (devApiResponse != null)
                {
                    articles.AddRange(devApiResponse);
                }
                pageNumber += 1;
            }
        }
    }
}