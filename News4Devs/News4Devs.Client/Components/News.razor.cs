using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Helpers;
using News4Devs.Client.Models;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class News
    {
        [Inject]
        protected IHttpClientService HttpClientService { get; set; }

        [Inject]
        protected IToastService ToastService { get; set; }

        [Inject]
        protected IAuthenticationService AuthService { get; set; }

        [Parameter]
        public List<ArticleDto> Articles { get; set; }

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

        private async Task SaveArticleAsync(ArticleDto article)
        {
            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(article);
            string userId = await AuthService.GetCurrentUserIdAsync();

            var response = await HttpClientService.PostAsync<SavedArticleDto>(
                $"{ClientConstants.BaseUrl}v1/articles/{userId}/save", byteArrayContent);

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
    }
}