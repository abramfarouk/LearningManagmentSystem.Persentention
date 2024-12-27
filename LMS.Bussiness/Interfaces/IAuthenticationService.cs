using LMS.Bussiness.Dtos.AuthenticationDtos;
using LMS.Bussiness.DTOS.AuthenticationDtos;

namespace LMS.Bussiness.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<GResponse<string>> ConfirmEmailAsync(ConfirmEmailRequest request);
        public Task<GResponse<JwtAuthResponse>> SignIn(SignInRequest request);
        public Task<GResponse<JwtAuthResponse>> RefreshToken(RefreshTokenRequest request);
        public Task<GResponse<string>> IsValidToken(string accessToken);

    }
}
