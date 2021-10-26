using Microsoft.AspNetCore.Mvc;
using News4Devs.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class NewsController : News4DevsController
    {
        private readonly INewsCatcherApiService newsService;

        public NewsController(INewsCatcherApiService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> Test([FromQuery] string q, [FromQuery] string lang, [FromQuery] string page)
        {
            // https://localhost:44347/api/v1/news?q=programming&lang=en&page=2
            var queryParams = new Dictionary<string, string>()
            {
                { nameof(q), q },
                { nameof(lang), lang },
                { nameof(page), page }
            };

            var result = await newsService.GetNewsAsync(queryParams);
            return Ok(result);
        }
    }
}
