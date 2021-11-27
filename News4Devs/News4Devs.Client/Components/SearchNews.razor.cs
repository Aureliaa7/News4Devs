using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class SearchNews : ArticlesBase
    {
        private string searchedTags;
        private string[] tags;

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            SetSearchedTags();

            await GetArticlesAsync();
            loading = false;
        }

        private void SetSearchedTags()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("searchedWords", out var searchedWords))
            {
                searchedTags = searchedWords;
                tags = searchedWords.ToString().Split(' ', ',');
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
            pageNumber = 1;
        }

        protected override Task<string> GetUrlAsync()
        {
            /* If the user enters only one tag and I call the Dev API using the tags query, then the 
             * API won't return the right articles. So, it's mandatory to use tags only if there are more than 
             * one tag.
             */
            string url = string.Empty;
            if (tags.Length > 1)
            {
                url = $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}&tags={searchedTags}";
            }
            else
            {
                url = $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}&tag={searchedTags}";
            }

            return Task.FromResult(url);
        }
    }
}
