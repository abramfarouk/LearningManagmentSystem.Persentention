using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.UserDto;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagmentSystem.Controllers
{

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet(Router.UserRouting.List)]
        public async Task<IActionResult> GetStudentListAsync()
        {
            var response = await _userService.GetUserListAsync();
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet(Router.UserRouting.Prefix)]
        public async Task<IActionResult> GetUsersPaginated([FromQuery] UserPaginatedListRequest request)
        {
            var response = await _userService.GetUserPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }
        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetStudentByIdAsync(int StdId)
        {
            var response = await _userService.GetUserByIdAsync(StdId);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost(Router.UserRouting.ChangePassword)]
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
        [HttpPost(Router.UserRouting.AddStudent)]
        public async Task<IActionResult> AddStudent([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request, "student");
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost(Router.UserRouting.AddTeacher)]
        public async Task<IActionResult> AddTeacher([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request, "teacher");
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPut(Router.UserRouting.Edit)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateUserRequest request)
        {
            var response = await _userService.UpdateUserAsync(request);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> DeleteUserAsync(int Id)
        {
            var response = await _userService.DeleteUserAsync(Id);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

    }




}
