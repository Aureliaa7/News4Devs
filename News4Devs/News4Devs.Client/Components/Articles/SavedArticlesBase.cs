using News4Devs.Core.DTOs;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public abstract class SavedArticlesBase : ArticlesBase
    {
        protected override async Task GetArticlesAsync()
        {
            string url = await GetUrlAsync();
            var response = await HttpClientService.GetAsync<PagedResponseDto<ExtendedArticleDto>>(url);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ToastService.ShowError("An unexpected error occurred during retrieving the articles");
            }
            else
            {
                var extendedArticlesDtos = response.Data.Data;
                if (extendedArticlesDtos.Any())
                {
                    Articles.AddRange(extendedArticlesDtos);
                }
                else
                {
                    isLoadMoreButtonVisible = false;
                }
                pageNumber++;
            }

            CheckIfThereAreMoreArticles(response.Data);
        }

        private void CheckIfThereAreMoreArticles(PagedResponseDto<ExtendedArticleDto> pagedResponse)
        {
            if (pagedResponse.NextPage == null)
            {
                isLoadMoreButtonVisible = false;
            }
        }
    }
}
