using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.AuthorizationDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        public AuthorizationController(IAuthorizationService authService)
        {
            _authService = authService;
        }
        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRoleAsync(AddRoleRequest request)
        {
            var response = await _authService.AddRoleAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRoleAsync(int roleId)
        {
            var response = await _authService.DeleteRoleAsync(roleId);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet(Router.AuthorizationRouting.GetRoles)]
        public async Task<IActionResult> GetRolesAsync()
        {
            var response = await _authService.GetRolesAsync();
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleByIdAsync(int roleId)
        {
            var response = await _authService.GetRoleByIdAsync(roleId);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdatedRole)]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleRequest request)
        {
            var response = await _authService.UpdateRoleAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
