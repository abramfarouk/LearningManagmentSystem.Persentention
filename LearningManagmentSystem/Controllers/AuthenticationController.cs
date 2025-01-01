using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.AuthenticationDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagmentSystem.Controllers
{

    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost(Router.AuthenicationRouting.SignIn)]
        public async Task<IActionResult> SignInAsync(SignInRequest request)
        {
            var response = await _authService.SignIn(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);

        }

        [HttpGet(Router.AuthenicationRouting.IsValidToken)]
        public async Task<IActionResult> IsValidToken([FromQuery] string accessToken)
        {
            var response = await _authService.IsValidToken(accessToken);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost(Router.AuthenicationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshToken(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet(Router.AuthenicationRouting.ConfirmEmail)]
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

        [HttpPost(Router.AuthenicationRouting.SendResetPasswordCode)]
        public async Task<IActionResult> SendResetPasswordCodeAsync([FromBody] ResetPasswordRequest request)
        {
            var response = await _authService.SendResetPasswordCode(request.Email);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost(Router.AuthenicationRouting.ConfirmResetPassword)]
        public async Task<IActionResult> ConfirmResetPasswordAsync([FromBody] ConfirmResetPasswordRequest request)
        {
            var response = await _authService.ConfirmResetPassword(request.Code, request.Email);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost(Router.AuthenicationRouting.ResetPassword)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto request)
        {
            var response = await _authService.ResetPassword(request.Email, request.Password);
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
