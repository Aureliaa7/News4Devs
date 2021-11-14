using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class News
    {
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
    }
}