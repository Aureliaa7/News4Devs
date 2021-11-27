using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class SavedArticles : ArticlesBase
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
            return $"{ClientConstants.BaseUrl}v1/articles/{userId}/saved";
        }

        protected override Task CheckIfThereAreMoreArticlesAsync()
        {
            // do nothing for now

            //TODO update when adding pagination
            return Task.Delay(10);
        }

        private async Task LoadMoreSavedArticles()
        {
            await GetArticlesAsync();
        }
    }
}
