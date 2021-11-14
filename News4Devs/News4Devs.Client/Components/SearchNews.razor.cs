using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class SearchNews : NewsBase
    {
        private string searchedTags;
        private string[] tags;

        protected override async Task OnInitializedAsync()
        {
            SetSearchedTags();
            await GetArticlesAsync();
        }

        private async Task LoadMoreArticles()
        {
            await GetArticlesAsync();
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
            Reset(searchedWords);
            SetSearchedTags();
            await GetArticlesAsync();
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

        protected override string GetUrl()
        {
            /* If the user enters only one tag and I call the Dev API using the tags query, then the 
             * API won't return the right articles. So, it's mandatory to use tags only if there are more than 
             * one tag.
             */

            if (tags.Length > 1)
            {
                return $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}&tags={searchedTags}";
            }
            return $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}&tag={searchedTags}";
        }
    }
}
