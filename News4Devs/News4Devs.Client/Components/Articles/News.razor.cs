using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Helpers;
using News4Devs.Client.Models;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Articles
{
    public partial class News
    {
        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IAuthenticationService AuthService { get; set; }

        [Parameter]
        public List<ExtendedArticleDto> Articles { get; set; }

        [Parameter]
        public EventCallback OnLoadMoreArticles { get; set; }

        [Parameter]
        public bool IsLoadMoreButtonVisible { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        private readonly string savedArticlesEndpoint = "saved";
        private readonly string favoriteArticlesEndpoint = "favorite";
        private readonly string saveArticleEndpoint = "save";

        private async Task LoadMoreArticles()
        {
            await OnLoadMoreArticles.InvokeAsync();
        }

        private async Task SaveArticleAsync(ExtendedArticleDto extendedArticle)
        {
            await MarkAsSavedOrFavoriteArticleAsync(extendedArticle, saveArticleEndpoint);
        }

        private async Task MarkAsFavoriteAsync(ExtendedArticleDto extendedArticle)
        {
            await MarkAsSavedOrFavoriteArticleAsync(extendedArticle, favoriteArticlesEndpoint);
        }

        private async Task MarkAsSavedOrFavoriteArticleAsync(ExtendedArticleDto extendedArticle, string endpoint)
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(extendedArticle.Article);
            string userId = await AuthService.GetCurrentUserIdAsync();

            var response = await HttpClientService.PostAsync<SavedArticleDto>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/{endpoint}", byteArrayContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (endpoint == saveArticleEndpoint)
                {
                    extendedArticle.IsSaved = true;
                }
                else
                {
                    extendedArticle.IsFavorite = true;
                }
            }

            HandleResponse(response);
        }

        private void HandleResponse(ApiResponse<SavedArticleDto> response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ToastService.ShowSuccess("The article was successfully saved");
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                ToastService.ShowInfo("This article was already saved...");
            }
            else
            {
                ToastService.ShowError("The article could not be saved...");
            }
        }

        private async Task RemoveFromSavedArticlesAsync(ExtendedArticleDto extendedArticle)
        {
            await RemoveArticleAsync(extendedArticle, savedArticlesEndpoint);
        }

        private async Task RemoveFromFavoriteArticlesAsync(ExtendedArticleDto extendedArticle)
        {
            await RemoveArticleAsync(extendedArticle, favoriteArticlesEndpoint);
        }

        private async Task RemoveArticleAsync(ExtendedArticleDto extendedArticle, string endpoint)
        {
            string userId = await AuthService.GetCurrentUserIdAsync();
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(
             new DeleteSavedArticleDto
             {
                 ArticleTitle = extendedArticle.Article.title
             });

            var response = await HttpClientService.DeleteAsync<string>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/{endpoint}", byteArrayContent);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                if (endpoint == savedArticlesEndpoint)
                {
                    extendedArticle.IsSaved = false;
                }
                else
                {
                    extendedArticle.IsFavorite = false;
                }
            }
            else
            {
                ToastService.ShowError("The article could not be removed from list...");
            }
        }
    }
}