using News4Devs.Core.Enums;
using System;

namespace News4Devs.Core.DTOs
{
    public class SavedArticleDto
    {
        public Guid Id { get; set; }

        public ArticleSavingType ArticleSavingType { get; set; }

        public ArticleDto Article { get; set; }
    }
}
