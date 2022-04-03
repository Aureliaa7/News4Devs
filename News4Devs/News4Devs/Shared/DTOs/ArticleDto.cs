using System.ComponentModel.DataAnnotations;

namespace News4Devs.Shared.DTOs
{
    public class ArticleDto
    {
        [Required]
        public string author { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string url { get; set; }

        [Required]
        public string urlToImage { get; set; }

        [Required]
        public string publishedAt { get; set; }

        [Required]
        public string content { get; set; }
    }
}
