using News4Devs.Core.Entities;
using News4Devs.Core.Enums;
using System;

namespace News4Devs.Core.Models
{
    public class SaveArticleModel
    {
        public Article Article { get; set; }

        public ArticleSavingType ArticleSavingType { get; set; }

        public Guid UserId { get; set; }
    }
}
