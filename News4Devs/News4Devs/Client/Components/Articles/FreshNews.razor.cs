﻿using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public partial class FreshNews : ArticlesBase
    {
        //TODO uncomment
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            //await GetArticlesAsync();
            loading = false;
        }

        protected override Task<string> GetUrlAsync()
        {
            return Task.FromResult($"{ClientConstants.BaseUrl}/articles?page={pageNumber}&state=fresh");
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
