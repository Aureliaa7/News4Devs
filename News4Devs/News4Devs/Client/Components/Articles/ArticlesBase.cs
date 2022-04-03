using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public abstract class ArticlesBase : ComponentBase
    {
        [Inject]
        protected IHttpClientService HttpClientService { get; set; }

        [Inject]
        protected IToastService ToastService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }


        [Inject]
        protected IAuthenticationService AuthService { get; set; }

        protected List<ExtendedArticleDto> Articles = new();

        protected int pageNumber = 1;

        protected bool isLoadMoreButtonVisible = true;
       
        protected bool loading;

        private int totalArticles;

        protected abstract Task<string> GetUrlAsync();

        protected virtual async Task GetArticlesAsync()
        {
            string url = await GetUrlAsync();
            var response = await HttpClientService.GetAsync<NewsApiResponseModel>(url);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ToastService.ShowError("An unexpected error occurred during retrieving the articles");
            }
            else
            {
                totalArticles = response.Data.TotalResults;
                if (response.Data.Articles.Any())
                {
                    Articles.AddRange(response.Data.Articles);
                }
                pageNumber++;
            }
            CheckIfThereAreMoreArticles();
        }

        private void CheckIfThereAreMoreArticles()
        {
            isLoadMoreButtonVisible = pageNumber * ClientConstants.MaxPageSize < totalArticles;
        }

        protected Task<string> GetCurrentUserIdAsync()
        {
            return AuthService.GetCurrentUserIdAsync();
        }

        protected virtual async Task LoadMoreArticlesAsync()
        {
            await GetArticlesAsync();
        }
    }
}
