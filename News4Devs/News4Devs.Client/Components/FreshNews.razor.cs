using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class FreshNews : NewsBase
    {
        protected override async Task OnInitializedAsync()
        {
            await GetArticlesAsync();
        }

        private async Task LoadMoreFreshArticles()
        {
            await GetArticlesAsync();
        }

        protected override string GetUrl()
        {
            return $"{ClientConstants.BaseUrl}v1/articles?page={pageNumber}&state=fresh";
        }

        private void RedirectToSearchNewsPage(string searchedWords)
        {
            var query = new Dictionary<string, string> {
                { "searchedWords", $"{searchedWords}" } 
            };
            NavigationManager.NavigateTo(QueryHelpers.AddQueryString("/search-news", query));
        }
    }
}
