using LMS.Bussiness.DTOS.AuthorizationDtos;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LMS.Bussiness.Implementation
{
    public class AuthorizationService : ResponseHandler, IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<GResponse<string>> AddRoleAsync(AddRoleRequest request)
        {
            try
            {

                var role = await _roleManager.FindByNameAsync(request.RoleName);
                if (role != null)
                {
                    return BadRequest<string>("Role already exists");
                }
                role = new Role
                {
                    Name = request.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return OK<string>("Role added successfully");
                }
                else
                {
                    return BadRequest<string>("Role not added");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid an errors {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return BadRequest<string>("Role not found");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return OK<string>("Role deleted successfully");
            }
            else
            {
                return BadRequest<string>("Role not deleted");
            }
        }

        public async Task<GResponse<RoleResponseDto>> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return NotFound<RoleResponseDto>("Role not found");
            }
            var roleDto = new RoleResponseDto
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            return OK<RoleResponseDto>(roleDto);

        }

        public async Task<GResponse<List<RoleResponseDto>>> GetRolesAsync()
        {
            var roles = _roleManager.Roles.ToList();
            if (roles == null)
            {
                return NotFound<List<RoleResponseDto>>("Roles not found");
            }
            var rolesDto = roles.Select(role => new RoleResponseDto
            {
                RoleId = role.Id,
                RoleName = role.Name
            }).ToList();
            return OK<List<RoleResponseDto>>(rolesDto);
        }

        public async Task<GResponse<string>> UpdateRoleAsync(UpdateRoleRequest request)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                if (role == null)
                {
                    return BadRequest<string>("Role not found");
                }
                role.Name = request.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return OK<string>("Role updated successfully");
                }
                else
                {
                    return BadRequest<string>("Role not updated");
                }

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid an errors {ex.Message}");
            }


        }



    }
}
