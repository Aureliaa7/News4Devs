using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public partial class SearchNews : ArticlesBase
    {
        private string[] tags;
        private string searched;

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
                tags = searchedWords.ToString().Split(' ', ',', '.');
                searched = string.Empty;
                for (int i = 0; i < tags.Count()-1; i++)
                {
                    searched += $"{tags[i]}+or+";
                }

                var a = tags.LastOrDefault();

                searched += tags.LastOrDefault();
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
            return Task.FromResult($"{ClientConstants.BaseUrl}/articles?page={pageNumber}&q={searched}");
        }
    }
}
