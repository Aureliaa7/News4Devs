using Microsoft.AspNetCore.Mvc;

namespace News4Devs.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class News4DevsController : ControllerBase { }
}
