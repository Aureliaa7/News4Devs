using News4Devs.Core.Enums;
using System;

namespace News4Devs.Core.Entities
{
    public class SavedArticle
    {
        public Guid Id { get; set; }

        public ArticleSavingType ArticleSavingType { get; set; }

        public Guid UserId { get; set; }

        public string ArticleTitle { get; set; }


        public Article Article { get; set; }

        public User User { get; set; }
    }
}
