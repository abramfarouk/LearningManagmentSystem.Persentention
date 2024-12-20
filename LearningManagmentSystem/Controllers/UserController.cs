using LMS.Bussiness.DTOS.UserDto;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetStudentListAsync()
        {
            var response = await _userService.GetUserListAsync();
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetUsersPaginated([FromQuery] UserPaginatedListRequest request)
        {
            var response = await _userService.GetUserPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetStudentByIdAsync(int StdId)
        {
            var response = await _userService.GetUserByIdAsync(StdId);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDto request)
        {
            var response = await _userService.ChangeUserPasswordAsync(request);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);


            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("Add-Student")]
        public async Task<IActionResult> AddStudent([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request, "student");
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Update-Student")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateUserRequest request)
        {
            var response = await _userService.UpdateUserAsync(request);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("Remove-Student")]
        public async Task<IActionResult> DeleteUserAsync(int StudentId)
        {
            var response = await _userService.DeleteUserAsync(StudentId);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
