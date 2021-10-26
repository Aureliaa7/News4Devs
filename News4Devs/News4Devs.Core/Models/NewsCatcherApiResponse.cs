using News4Devs.Core.Entities;
using System.Collections.Generic;

namespace News4Devs.Core.Models
{
    public class NewsCatcherApiResponse
    {
        public int Page { get; set; }

        public int TotalPages { get; set; }
        
        public int PageSize { get; set; }
        
        public List<Article> Articles { get; set; }
    }
}
