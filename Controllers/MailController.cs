using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertiesListings.Helpers;
using PropertiesListings.Services;

namespace PropertiesListings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAysnc(request);
                return Ok(request);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> GetEmail()
        {
            string message = "Hello all";
            return Ok(message);
        }
    }
}
