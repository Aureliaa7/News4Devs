using System.ComponentModel.DataAnnotations;

namespace News4Devs.Shared.DTOs
{
    public class DeleteSavedArticleDto
    {
        [Required]
        public string ArticleTitle { get; set; }
    }
}
