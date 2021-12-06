using News4Devs.Shared.Entities;
using News4Devs.Shared.Enums;
using System;

namespace News4Devs.Shared.Models
{
    public class SaveArticleModel
    {
        public Article Article { get; set; }

        public ArticleSavingType ArticleSavingType { get; set; }

        public Guid UserId { get; set; }
    }
}
