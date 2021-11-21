using System.ComponentModel.DataAnnotations;

namespace News4Devs.Core.Entities
{
    public class Article
    {
        [Key]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ReadablePublishDate { get; set; }

        public string Url { get; set; }

        public string PublishedAt { get; set; }

        public string SocialImageUrl { get; set; }

        public int ReadingTimeMinutes { get; set; }

        public string Tags { get; set; }

        public string AuthorName { get; set; }

        public string AuthorWebsiteUrl { get; set; }
    }
}
