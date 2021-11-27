using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public partial class Article
    {
        [Parameter]
        public ExtendedArticleDto ExtendedArticleDto { get; set; }

        [Parameter]
        public EventCallback<ExtendedArticleDto> SaveArticle { get; set; }

        [Parameter]
        public EventCallback<ExtendedArticleDto> MarkAsFavorite { get; set; }

        [Parameter]
        public EventCallback<string> RemoveFromSavedArticles { get; set; }

        [Parameter]
        public EventCallback<string> RemoveFromFavoriteArticles { get; set; }

        private async Task OnSaveArticle()
        {
            await SaveArticle.InvokeAsync(ExtendedArticleDto);
        }

        private async Task OnRemoveFromSavedArticles()
        {
            await RemoveFromSavedArticles.InvokeAsync(ExtendedArticleDto.Article.title);
        }

        private async Task OnRemoveFromFavoriteArticles()
        {
            await RemoveFromFavoriteArticles.InvokeAsync(ExtendedArticleDto.Article.title);
        }

        private async Task OnMarkArticleAsFavorite()
        {
            await MarkAsFavorite.InvokeAsync(ExtendedArticleDto);
        }
    }
}