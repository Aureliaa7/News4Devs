﻿using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class Article
    {
        [Parameter]
        public ExtendedArticleDto ExtendedArticleDto { get; set; }

        [Parameter]
        public EventCallback<ExtendedArticleDto> SaveArticle { get; set; }

        [Parameter]
        public EventCallback<ExtendedArticleDto> MarkAsFavorite { get; set; }

        // send the whole object(even if only the title is needed) to avoid making another request in order to update the UI
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