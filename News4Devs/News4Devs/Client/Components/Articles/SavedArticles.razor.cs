﻿using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public partial class SavedArticles : SavedArticlesBase
    {
        protected override async Task OnInitializedAsync()
        {
            loading = true;
            await GetArticlesAsync();
            loading = false;
        }

        protected async override Task<string> GetUrlAsync()
        {
            string userId = await GetCurrentUserIdAsync();
            return $"{ClientConstants.BaseUrl}/articles/{userId}/saved?pageNumber={pageNumber}&pageSize={ClientConstants.MaxPageSize}";
        }
    }
}
