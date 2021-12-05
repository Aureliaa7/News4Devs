using News4Devs.Core.Entities;

namespace News4Devs.Core.Models
{
    public class ExtendedArticleModel
    {
        public Article Article { get; set; }

        public bool IsSaved { get; set; }

        public bool IsFavorite { get; set; }
    }
}
