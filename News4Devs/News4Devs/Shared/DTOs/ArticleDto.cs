using System.ComponentModel.DataAnnotations;

namespace News4Devs.Shared.DTOs
{
    // Model used to catch data from Dev API.
    // The property names must be the same as those used by the Dev.to API.
    public class ArticleDto
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string readable_publish_date { get; set; }

        [Required]
        public string url { get; set; }

        [Required]
        public string published_at { get; set; }

        [Required]
        public string social_image { get; set; }

        [Required]
        public int reading_time_minutes { get; set; }

        [Required]
        public string tags { get; set; }

        [Required]
        public DevUserDto user { get; set; }
    }
}
