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
        private int numberOfTotalPages;
        private int pageNumber = 1;

        protected async override Task OnInitializedAsync()
        {
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<NewsCatcherApiResponseDto>(url);

            HandleApiResponse(response);
        }

        private async Task LoadMoreArticles()
        {
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<NewsCatcherApiResponseDto>(url);

            HandleApiResponse(response);
        }

        private int GetNextPageNumber()
        {
            if (pageNumber + 1 < numberOfTotalPages) 
            {
                return pageNumber + 1;
            }
            return pageNumber;
        }

        private string GetUrl()
        {
            return $"{ClientConstants.BaseUrl}v1/news/?q=dotnet&lang=en&page={pageNumber}";
        }

        private void HandleApiResponse(ApiResponse<NewsCatcherApiResponseDto> response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ToastService.ShowError("An unexpected error occured during retrieving the news");
            }
            else
            {
                var newsCatcherApiResponse = response.Data;
                numberOfTotalPages = newsCatcherApiResponse.total_pages;
                if (newsCatcherApiResponse.articles != null)
                {
                    articles.AddRange(newsCatcherApiResponse.articles);
                }
                // compute the next page number
                pageNumber = GetNextPageNumber();
            }
        }
    }
}
