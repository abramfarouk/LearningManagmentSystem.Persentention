using LMS.Bussiness.DTOS.AuthorizationDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        public AuthorizationController(IAuthorizationService authService)
        {
            _authService = authService;
        }
        [HttpPost("Add-Role")]
        public async Task<IActionResult> AddRoleAsync(AddRoleRequest request)
        {
            var response = await _authService.AddRoleAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("Delete-Role")]

        public async Task<IActionResult> DeleteRoleAsync(int roleId)
        {
            var response = await _authService.DeleteRoleAsync(roleId);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("Get-Roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var response = await _authService.GetRolesAsync();
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("Get-Role-By-Id")]
        public async Task<IActionResult> GetRoleByIdAsync(int roleId)
        {
            var response = await _authService.GetRoleByIdAsync(roleId);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut("Update-Role")]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleRequest request)
        {
            var response = await _authService.UpdateRoleAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
