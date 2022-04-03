using News4Devs.Shared.DTOs;
using System.Collections.Generic;

namespace News4Devs.Shared.Models
{
    public class NewsApiResponseModel
    {
        public string Status { get; set; }

        public int TotalResults { get; set; }

        public List<ExtendedArticleDto> Articles { get; set; }
    }
}
