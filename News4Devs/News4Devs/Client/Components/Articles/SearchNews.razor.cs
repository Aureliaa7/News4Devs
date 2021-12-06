using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public partial class SearchNews : ArticlesBase
    {
        private string searchedTags;
        private string[] tags;
        private List<ExtendedArticleDto> foundArticles = new();
        private readonly int noArticlesToBeRetrieved = 30;

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            SetSearchedTags();
            await GetArticlesAsync();
            loading = false;
        }

        protected override async Task GetArticlesAsync()
        {
            if (tags.Length > 1)
            {
                foundArticles = await GetArticlesListAsync();
                Articles.AddRange(foundArticles.Take(noArticlesToBeRetrieved));
                foundArticles.RemoveRange(0, noArticlesToBeRetrieved);
            }
            else
            {
                await base.GetArticlesAsync();
            }
        }

        protected override async Task LoadMoreArticlesAsync()
        {
            if (tags.Length > 1)
            {
                if (foundArticles.Count > 0)
                {
                    Articles.AddRange(foundArticles.Take(noArticlesToBeRetrieved));
                    foundArticles.RemoveRange(0, noArticlesToBeRetrieved);
                }
                else
                {
                    await GetArticlesAsync();
                }
            }
            else
            {
                await base.GetArticlesAsync();
            }
        }

        /* Since the Dev API is no longer able to return articles based on more than one tag(using the 'tags' query param), 
           I need an alternative. Therefore, if the search box contains more than one string, call this method. 
           Otherwise, call the method from the base class. In this method, for each word in the search box, make a call to 
           api and get the articles, store these articles in a list, remove the duplicated articles, shuffle them, take the 
           first 30 articles and add them in Articles, remove them from foundArticles list.
         */
        private async Task<List<ExtendedArticleDto>> GetArticlesListAsync()
        {
            var articles = new List<ExtendedArticleDto>();

            foreach (var tag in tags)
            {
                string url = $"{ClientConstants.BaseUrl}/articles?page={pageNumber}&tag={tag}";
                var response = await HttpClientService.GetAsync<IList<ExtendedArticleDto>>(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    articles.AddRange(response.Data);
                }
            }
            pageNumber++;

            articles = articles.Distinct().ToList();
            return articles.Randomize();
        }

        private void SetSearchedTags()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("searchedWords", out var searchedWords))
            {
                searchedTags = searchedWords;
                tags = searchedWords.ToString().Split(' ', ',', '.');
            }
        }

        private async Task SearchArticles(string searchedWords)
        {
            loading = true;
            Reset(searchedWords);
            SetSearchedTags();
            await GetArticlesAsync();
            loading = false;
        }

        private void Reset(string newSearchedWords)
        {
            var query = new Dictionary<string, string> {
                { "searchedWords", $"{newSearchedWords}" }
            };
            // navigate to this page in order to set the new value for query param
            NavigationManager.NavigateTo(QueryHelpers.AddQueryString("/search-news", query));
            Articles.Clear();
            foundArticles.Clear();
            pageNumber = 1;
        }

        protected override Task<string> GetUrlAsync()
        {
            return Task.FromResult($"{ClientConstants.BaseUrl}/articles?page={pageNumber}&tag={searchedTags}");
        }
    }
}
