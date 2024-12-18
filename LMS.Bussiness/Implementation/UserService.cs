using LMS.Bussiness.DTOS.UserDto;
using LMS.Data.Bases;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LMS.Bussiness.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GResponse<string>> AddUserAsync(AddUserRequest request)
        {
            var user = _userManager.FindByNameAsync(request.FirstName);
            if (user != null)
            {
                return ErrorResponse($"the UserName {request.FirstName} Is Already Exist");
            }
            var Email = _userManager.FindByEmailAsync(request.Email);
            if (Email != null)
            {
                return ErrorResponse($"the Email {request.Email} Is Already Exist");
            }


            return ErrorResponse($"the Email {request.Email} Is Already Exist");


        }

        public Task<GResponse<string>> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<UserResponseDto>> GetUserByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<IEnumerable<UserResponseDto>>> GetUserListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<PigatedResult<UserResponseDto>>> GetUserPaginatedListAsync(UserPaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdateUserAsync(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }


        private GResponse<string> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {

            return new GResponse<string>
            {
                StatusCode = statusCode,
                Message = message,
                IsSuccess = false,
            };


        }
    }
}


