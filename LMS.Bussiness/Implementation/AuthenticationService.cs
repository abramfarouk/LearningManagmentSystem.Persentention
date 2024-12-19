using LMS.Bussiness.DTOS.AuthenticationDtos;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LMS.Bussiness.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        public AuthenticationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<GResponse<string>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.userId.ToString());
                if (user == null)
                {
                    return new GResponse<string>()
                    {
                        IsSuccess = false,
                        Message = $"No found user with Id : {request.userId}.",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }


                var decodeCode = WebUtility.UrlDecode(request.code);
                var confirmEmail = await _userManager.ConfirmEmailAsync(user, decodeCode);
                if (!confirmEmail.Succeeded)
                {
                    return new GResponse<string>()
                    {
                        IsSuccess = false,
                        Message = GetErrors(confirmEmail.Errors),
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                return new GResponse<string>()
                {
                    IsSuccess = true,
                    Message = "Confirm Email Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new GResponse<string>()
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

        }

        private string GetErrors(IEnumerable<IdentityError> errors)
        {
            return "An Error " + string.Join(", ", errors.Select(x => x.Description));
        }
    }
}
