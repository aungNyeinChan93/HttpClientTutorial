using HttpClient.domain.Features.Email;
using HttpClient.domain.Features.Email.ReqResModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClient.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendAsync(EmailRequest request)
        {
            var response = await _emailService.SendAsync(request);
            return Ok(response);
        }
    }
}
