namespace News4Devs.Shared.Models
{
    public class NewsApiQueryParamsModel
    {
        public string apiKey { get; set; }

        // Keywords or phrases to search for in the article title and body
        public string q { get; set; }

        // The fields to restrict your q search to. The possible options are: title, description, content
        public string searchIn { get; set; }

        public string sources { get; set; } 

        public string domains { get; set; }

        public string excludeDomains { get; set; }

        public string from { get; set; }  

        public string to { get; set; }

        public string language { get; set; }

        public string sortBy { get; set; }  

        public int? pageSize { get; set; }

        public int? page { get; set; }  
    }
}
