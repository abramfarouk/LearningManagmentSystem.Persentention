namespace LMS.Bussiness.Interfaces
{
    public interface IEmailService
    {
        Task<GResponse<string>> SendEmailAsync(string email, string mess);
    }
}
