using LMS.Bussiness.DTOS.AuthenticationDtos;

namespace LMS.Bussiness.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<GResponse<string>> ConfirmEmailAsync(ConfirmEmailRequest request);

    }
}
