using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Router.Core.Models;

namespace Router.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [Route("sendSms")]
        public async Task<IActionResult> SendSmsAsync([FromBody] InputMessage inputMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
