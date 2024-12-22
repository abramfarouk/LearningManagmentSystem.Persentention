namespace LMS.Bussiness.Bases
{
    public class JwtSettings
    {
        public string? Secret { get; set; }
        public string? Issure { get; set; }
        public string? Audience { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssure { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssureSigningKey { get; set; }
        public int AccessTokenExpireDate { get; set; }
        public int RefreshTokenExpireDate { get; set; }
    }
}
