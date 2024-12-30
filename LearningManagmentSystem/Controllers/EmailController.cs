using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.EmailDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost(Router.EmailRouting.SendEmail)]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailDto request)
        {
            var result = await _emailService.SendEmailAsync(request.Email, request.Mess);
            if (result.IsSuccess)
            {
                return Ok("Email sent successfully");
            }
            return BadRequest("Email not sent");
        }
    }
}
