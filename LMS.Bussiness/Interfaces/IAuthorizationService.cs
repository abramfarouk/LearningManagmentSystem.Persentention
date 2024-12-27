using LMS.Bussiness.DTOS.AuthorizationDtos;

namespace LMS.Bussiness.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<GResponse<string>> AddRoleAsync(AddRoleRequest request);
        public Task<GResponse<string>> UpdateRoleAsync(UpdateRoleRequest request);
        public Task<GResponse<string>> DeleteRoleAsync(int roleId);
        public Task<GResponse<List<RoleResponseDto>>> GetRolesAsync();
        public Task<GResponse<RoleResponseDto>> GetRoleByIdAsync(int roleId);


    }
}
