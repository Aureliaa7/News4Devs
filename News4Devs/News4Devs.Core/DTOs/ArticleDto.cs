namespace News4Devs.Core.DTOs
{
    // Model used to catch data from Dev API.
    // The property names must be the same as those used by the Dev.to API.
    public class ArticleDto
    {
        public string title { get; set; }

        public string description { get; set; }

        public string readable_publish_date { get; set; }

        public string url { get; set; }

        public string published_at { get; set; }

        public string social_image { get; set; }

        public int reading_time_minutes { get; set; }
       
        public string tags { get; set; }
      
        public DevUserDto user { get; set; }
    }
}
