using System.ComponentModel.DataAnnotations;

namespace News4Devs.Core.DTOs
{
    public class DeleteSavedArticleDto
    {
        [Required]
        public string ArticleTitle { get; set; }
    }
}
