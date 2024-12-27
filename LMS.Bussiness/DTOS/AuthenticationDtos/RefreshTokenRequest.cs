namespace LMS.Bussiness.DTOS.AuthenticationDtos
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
