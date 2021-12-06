using News4Devs.Shared.Entities;

namespace News4Devs.Shared.Models
{
    public class ExtendedArticleModel
    {
        public Article Article { get; set; }

        public bool IsSaved { get; set; }

        public bool IsFavorite { get; set; }
    }
}
