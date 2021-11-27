namespace News4Devs.Core.DTOs
{
    public class ExtendedArticleDto
    {
        public ArticleDto Article { get; set; }

        public bool IsSaved { get; set; }

        public bool IsFavorite { get; set; }
    }
}
