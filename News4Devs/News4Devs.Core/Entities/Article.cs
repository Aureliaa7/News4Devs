using System;

namespace News4Devs.Core.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string PublishedDate { get; set; }
        
        public string Link { get; set; }
        
        public string Media { get; set; }   

        public string Summary { get; set; }

        public string Author { get; set; }
    }
}
