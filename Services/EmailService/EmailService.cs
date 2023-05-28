using SendGrid.Helpers.Mail;
using SendGrid;
using DriveOfCity.IServices.IEmailService;

namespace DriveOfCity.Services.EmailService
{
    public class SendEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmail(string destinatario, string assunto, string conteudo)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("sr.degobi@gmail.com", "Thiago Degobi");
            var to = new EmailAddress(destinatario);
            var mensagem = MailHelper.CreateSingleEmail(from, to, assunto, conteudo, conteudo);
            await client.SendEmailAsync(mensagem);
        }

    }
}
