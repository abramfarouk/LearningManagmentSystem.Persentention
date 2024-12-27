using LMS.Bussiness.DTOS.AuthenticationDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Sign-In")]
        public async Task<IActionResult> SignInAsync(SignInRequest request)
        {
            var response = await _authService.SignIn(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);

        }

        [HttpGet("IsValid-Token")]
        public async Task<IActionResult> IsValidToken([FromQuery] string accessToken)
        {
            var response = await _authService.IsValidToken(accessToken);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshToken(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var response = await _authService.ConfirmEmailAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }


    }
}
