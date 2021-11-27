using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class Article
    {
        [Parameter]
        public ExtendedArticleDto ExtendedArticleDto { get; set; }

        [Parameter]
        public EventCallback<ArticleDto> SaveArticle { get; set; }

        [Parameter]
        public EventCallback<ArticleDto> MarkAsFavorite { get; set; }

        [Parameter]
        public EventCallback<string> RemoveArticle { get; set; }

        private async Task OnSaveArticle()
        {
            await SaveArticle.InvokeAsync(ExtendedArticleDto.Article);
        }

        private async Task OnRemoveArticle()
        {
            await RemoveArticle.InvokeAsync(ExtendedArticleDto.Article.title);
        }

        private async Task OnMarkArticleAsFavorite()
        {
            await MarkAsFavorite.InvokeAsync(ExtendedArticleDto.Article);
        }
    }
}