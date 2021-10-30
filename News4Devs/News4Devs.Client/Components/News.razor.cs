using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Models;
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

        protected async override Task OnInitializedAsync()
        {
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<IList<ArticleDto>>(url);

            HandleApiResponse(response);
        }

        private async Task LoadMoreArticles()
        {
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<IList<ArticleDto>>(url);

            HandleApiResponse(response);
        }

        private string GetUrl()
        {
            return $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}";
        }

        private void HandleApiResponse(ApiResponse<IList<ArticleDto>> response)
        {
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