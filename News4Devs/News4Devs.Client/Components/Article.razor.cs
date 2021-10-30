using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;

namespace News4Devs.Client.Components
{
    public partial class Article
    {
        [Parameter]
        public ArticleDto ArticleDto { get; set; }
    }
}