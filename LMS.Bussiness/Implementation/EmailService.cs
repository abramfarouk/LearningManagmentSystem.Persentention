




using MimeKit;

namespace LMS.Bussiness.Implementation
{
    public class EmailService : ResponseHandler, IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }



        public async Task<GResponse<string>> SendEmailAsync(string email, string mess)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);

                    var bodyBuilder = new BodyBuilder()
                    {
                        HtmlBody = $"{mess}",
                        TextBody = "Welcome"
                    };
                    var message = new MimeMessage()
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };

                    message.From.Add(new MailboxAddress("Learning Managment System ", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("To ", email));
                    message.Subject = "new Contact Submitted Data.";
                    //
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    return Success("Send Email Is Successfull !");
                }

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }


        }
    }
}
