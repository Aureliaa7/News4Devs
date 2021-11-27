using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Helpers;
using News4Devs.Client.Models;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Linq;
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

        private async Task LoadMoreArticles()
        {
            await OnLoadMoreArticles.InvokeAsync();
        }

        private async Task SaveArticleAsync(ExtendedArticleDto extendedArticle)
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(extendedArticle.Article);
            string userId = await AuthService.GetCurrentUserIdAsync();

            var response = await HttpClientService.PostAsync<SavedArticleDto>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/save", byteArrayContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                extendedArticle.IsSaved = true;
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

        private async Task MarkAsFavoriteAsync(ExtendedArticleDto extendedArticle)
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(extendedArticle.Article);
            string userId = await AuthService.GetCurrentUserIdAsync();

            var response = await HttpClientService.PostAsync<SavedArticleDto>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/favorite", byteArrayContent);

            // so that the changes reflect in UI 
            if (response.StatusCode == HttpStatusCode.OK)
            {
                extendedArticle.IsFavorite = true;
            }

            HandleResponse(response);
        }

        private async Task RemoveFromSavedArticlesAsync(string articleTitle)
        {
            string userId = await AuthService.GetCurrentUserIdAsync();
            var response = await HttpClientService.DeleteAsync<string>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/saved/{articleTitle}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                Articles = Articles.Except(Articles.Where(x => x.Article.title == articleTitle)).ToList();
                // or simply remove it from list
                ToastService.ShowSuccess("The article was successfully removed from Saved articles list");
            }
            else
            {
                ToastService.ShowError("The article could not be removed from Saved articles list...");
            }
        }

        private async Task RemoveFromFavoriteArticlesAsync(string articleTitle)
        {
            string userId = await AuthService.GetCurrentUserIdAsync();
            var response = await HttpClientService.DeleteAsync<string>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/favorite/{articleTitle}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                Articles = Articles.Except(Articles.Where(x => x.Article.title == articleTitle)).ToList();
                ToastService.ShowSuccess("The article was successfully removed from Favorite articles list");
            }
            else
            {
                ToastService.ShowError("The article could not be removed from Favorite articles list...");
            }
        }
    }
}