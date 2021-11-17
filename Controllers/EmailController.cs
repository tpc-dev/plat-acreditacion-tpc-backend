using Microsoft.AspNetCore.Mvc;
using PlatAcreditacionTPCBackend.Models;
using PlatAcreditacionTPCBackend.Servicios;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmailController: ControllerBase
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
