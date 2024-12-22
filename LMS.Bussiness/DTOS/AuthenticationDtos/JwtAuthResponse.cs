namespace LMS.Bussiness.Dtos.AuthenticationDtos
{
    public class JwtAuthResponse
    {
        public string? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
    public class RefreshToken
    {

        public string? UserName { get; set; }
        public DateTime ExpireAt { get; set; }
        public string? refreshTokenString { get; set; }
    }
}
