namespace LMS.Bussiness.DTOS.AuthenticationDtos
{
    public class ConfirmResetPasswordRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
