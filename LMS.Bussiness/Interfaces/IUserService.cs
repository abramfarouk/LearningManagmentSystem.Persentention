using LMS.Bussiness.DTOS.UserDto;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IUserService
    {
        //-------------------Command-------------
        public Task<GResponse<string>> AddUserAsync(AddUserRequest request);
        public Task<GResponse<string>> UpdateUserAsync(UpdateUserRequest request);
        public Task<GResponse<string>> DeleteUserAsync(int userId);

        //------------------Query--------------------
        public Task<GResponse<IEnumerable<UserResponseDto>>> GetUserListAsync();
        public Task<GResponse<PigatedResult<UserResponseDto>>> GetUserPaginatedListAsync(UserPaginatedListRequest request);
        public Task<GResponse<UserResponseDto>> GetUserByIdAsync();



    }
}
