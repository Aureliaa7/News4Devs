using News4Devs.Shared.Enums;
using System;

namespace News4Devs.Shared.DTOs
{
    public class SavedArticleDto
    {
        public Guid Id { get; set; }

        public ArticleSavingType ArticleSavingType { get; set; }

        public ArticleDto Article { get; set; }
    }
}
