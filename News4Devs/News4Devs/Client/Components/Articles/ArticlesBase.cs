using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
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
       
        protected bool loading = false;

        protected abstract Task<string> GetUrlAsync();

        protected virtual async Task GetArticlesAsync()
        {
            string url = await GetUrlAsync();
            var response = await HttpClientService.GetAsync<IList<ExtendedArticleDto>>(url);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ToastService.ShowError("An unexpected error occured during retrieving the articles");
            }
            else
            {
                if (response.Data.Any())
                {
                    Articles.AddRange(response.Data);
                }
                else
                {
                    isLoadMoreButtonVisible = false;
                }
                pageNumber++;
            }
            await CheckIfThereAreMoreArticlesAsync();
        }

        // Since the Dev API does not also return the number of total pages, I need a way to show the LoadMore button
        // only if there are more articles. So, after incrementing the number of the current page, I'll make a call
        // to Dev API to get the articles, but I won't add them in the Articles list(if there are any)
        private async Task CheckIfThereAreMoreArticlesAsync()
        {
            string url = await GetUrlAsync();
            var response = await HttpClientService.GetAsync<IList<ArticleDto>>(url);
            var articles = response.Data;
            if (!articles.Any())
            {
                isLoadMoreButtonVisible = false;
            }
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
