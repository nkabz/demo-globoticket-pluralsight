using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        [HttpGet(Name = "Index")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public OkResult Index()
        {
            return Ok();
        }
    }
}