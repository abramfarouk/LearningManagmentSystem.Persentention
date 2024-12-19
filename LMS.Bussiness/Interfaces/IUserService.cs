using LMS.Bussiness.DTOS.UserDto;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IUserService
    {
        //-------------------Command-------------
        public Task<GResponse<string>> AddUserAsync(AddUserRequest request, string roleName);
        public Task<GResponse<string>> UpdateUserAsync(UpdateUserRequest request);
        public Task<GResponse<string>> DeleteUserAsync(int userId);

        //------------------Query--------------------
        public Task<GResponse<IEnumerable<UserResponseDto>>> GetUserListAsync();
        public Task<PigatedResult<UserPaginatedListResponseDto>> GetUserPaginatedListAsync(UserPaginatedListRequest request);
        public Task<GResponse<UserResponseDto>> GetUserByIdAsync(int Id);



    }
}
