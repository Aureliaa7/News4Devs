using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class FavoriteArticles : ArticlesBase
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
            return $"{ClientConstants.BaseUrl}v1/articles/{userId}/favorite";
        }

        protected override Task CheckIfThereAreMoreArticlesAsync()
        {
            //TODO update when adding pagination
            return Task.Delay(10);
        }
    }
}
