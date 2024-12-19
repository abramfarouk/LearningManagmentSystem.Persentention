namespace LMS.Bussiness.DTOS.AuthenticationDtos
{
    public class ConfirmEmailRequest
    {
        public int userId { get; set; }
        public string? code { get; set; }
    }
}
