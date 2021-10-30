using Microsoft.AspNetCore.Mvc;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Models;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class ArticlesController : News4DevsController
    {
        private readonly IDevApiService devApiService;

        public ArticlesController(IDevApiService devApiService)
        {
            this.devApiService = devApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery] DevApiQueryParamsModel devApiQueryParamsModel)
        {
            var result = await devApiService.GetArticlesAsync(devApiQueryParamsModel);
            return Ok(result);
        }
    }
}
