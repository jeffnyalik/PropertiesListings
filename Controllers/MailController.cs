using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertiesListings.Helpers;
using PropertiesListings.Interfaces;
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
        //Send a welcome message
        [HttpPost("welcome")]
        public async Task<IActionResult> GetEmail()
        {
            await mailService.SendWelcomeEmailAsync("jeffnyak@gmail.com", "Email-Title", "Finally \n the app is working");
            return Ok("Success");
        }

        //send email with attachments
        [HttpPost("attachments")]
        public async Task<IActionResult> SendEmailAttachments([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailWithAttachmentAsync(request);
                return Ok(request);

            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Onboarding Welcoming message
        [HttpPost("onboarding")]
        public async Task<IActionResult> SendOnboardingMessage()
        {
            try
            {
                await mailService.SendOnboardingMessage("jeffnyak@gmail.com", "Karibu", "Karibu kaka");
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
