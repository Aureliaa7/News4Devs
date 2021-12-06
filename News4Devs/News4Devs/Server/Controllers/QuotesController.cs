using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    public class QuotesController : News4DevsController
    {
        private readonly IQuotableService quotableService;

        public QuotesController(IQuotableService quotableService)
        {
            this.quotableService = quotableService;
        }

        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> GetRandomQuote([FromQuery] QuotableApiQueryParamsModel queryParamsModel)
        {
            var randomQuote = await quotableService.GetRandomQuoteAsync(queryParamsModel);
            return Ok(randomQuote);
        }
    }
}
