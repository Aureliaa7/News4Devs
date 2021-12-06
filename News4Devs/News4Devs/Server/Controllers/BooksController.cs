using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.Interfaces.Services;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    public class BooksController : News4DevsController
    {
        private readonly IITBookstoreService bookstoreService;

        public BooksController(IITBookstoreService bookstoreService)
        {
            this.bookstoreService = bookstoreService;
        }

        [HttpGet]
        [Route("new")]
        public async Task<IActionResult> GetNewBooks()
        {
            var newBooks = await bookstoreService.GetNewBooksAsync();
            return Ok(newBooks);
        }
    }
}
