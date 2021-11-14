using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public abstract class NewsBase : ComponentBase
    {
        [Inject]
        protected IHttpClientService HttpClientService { get; set; }

        [Inject]
        protected IToastService ToastService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected List<ArticleDto> Articles = new List<ArticleDto>();

        protected int pageNumber = 1;

        protected bool isLoadMoreButtonVisible = true;
       
        protected bool loading = true;

        protected abstract string GetUrl();

        protected async Task GetArticlesAsync()
        {
            loading = true;
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<IList<ArticleDto>>(url);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ToastService.ShowError("An unexpected error occured during retrieving the news");
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
                pageNumber += 1;
            }
            loading = false; // no need to display the spinner while checking if there are more articles
            await CheckIfThereAreMoreArticlesAsync();
        }

        // Since the Dev API does not also return the number of total pages, I need a way to show the LoadMore button
        // only if there are more articles. So, after incrementing the number of the current page, I'll make a call
        // to Dev API to get the articles, but I won't add them in the Articles list(if there are any)
        private async Task CheckIfThereAreMoreArticlesAsync()
        {
            string url = GetUrl();
            var response = await HttpClientService.GetAsync<IList<ArticleDto>>(url);
            var articles = response.Data;
            if (!articles.Any())
            {
                isLoadMoreButtonVisible = false;
            }
        }
    }
}
