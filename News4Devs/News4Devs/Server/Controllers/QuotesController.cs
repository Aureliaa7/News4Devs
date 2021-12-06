using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.Interfaces.Services;
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
        public async Task<IActionResult> GetRandomQuote()
        {
            var randomQuote = await quotableService.GetRandomQuoteAsync();
            return Ok(randomQuote);
        }
    }
}
