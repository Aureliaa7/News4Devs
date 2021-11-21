using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Components
{
    public partial class Article
    {
        [Parameter]
        public ArticleDto ArticleDto { get; set; }

        [Parameter]
        public EventCallback<ArticleDto> SaveArticle { get; set; }

        private async Task OnSaveArticle()
        {
            await SaveArticle.InvokeAsync(ArticleDto);
        }
    }
}