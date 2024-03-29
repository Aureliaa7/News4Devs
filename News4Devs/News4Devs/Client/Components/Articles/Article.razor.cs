﻿using Microsoft.AspNetCore.Components;
using News4Devs.Shared.DTOs;
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
        public EventCallback<ExtendedArticleDto> RemoveFromSavedArticles { get; set; }

        [Parameter]
        public EventCallback<ExtendedArticleDto> RemoveFromFavoriteArticles { get; set; }

        private async Task OnSaveArticle()
        {
            await SaveArticle.InvokeAsync(ExtendedArticleDto);
        }

        private async Task OnRemoveFromSavedArticles()
        {
            await RemoveFromSavedArticles.InvokeAsync(ExtendedArticleDto);
        }

        private async Task OnRemoveFromFavoriteArticles()
        {
            await RemoveFromFavoriteArticles.InvokeAsync(ExtendedArticleDto);
        }

        private async Task OnMarkArticleAsFavorite()
        {
            await MarkAsFavorite.InvokeAsync(ExtendedArticleDto);
        }
    }
}