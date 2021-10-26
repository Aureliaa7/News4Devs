using System.Collections.Generic;

namespace News4Devs.Core.DTOs
{
    /* Note: Cannot use PascalCase for properties; I have to use the same names as the NewsCatcher API does
     otherwise the deserialization operation fails(for instance, the number page would be 0 and the articles list would be null). */
    public class NewsCatcherApiResponseDto
    {
        public int page { get; set; }

        public int total_pages { get; set; }

        public int page_size { get; set; }

        public List<ArticleDto> articles { get; set; }
    }
}
