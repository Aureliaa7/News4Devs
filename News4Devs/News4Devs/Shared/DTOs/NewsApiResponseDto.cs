using System.Collections.Generic;

namespace News4Devs.Shared.DTOs
{
    public class NewsApiResponseDto
    {
        public string status { get; set; }

        public int totalResults { get; set; }

        public List<ArticleDto> articles { get; set; }    
    }
}
