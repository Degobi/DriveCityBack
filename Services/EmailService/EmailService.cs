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

        public async Task EnviarEmail(string destinatario, string tipo)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("sr.degobi@gmail.com", "Thiago Degobi");
            var to = new EmailAddress(destinatario);

            SendGridMessage mensagem = null;
            var templateId = "";
            var data = new Dictionary<string, string>
            {
                { "nome_do_cliente", "Thiago" },
                { "valor_do_pagamento", "100,00" },
                { "numero_do_pedido", "123456" },
                { "data_do_pagamento", "01/01/2023" }
            };

            if (tipo == "PAGAMENTO")
            {
                templateId = "d-17ba8c4a0ab74e7b8c3ff3016b897d43";
                mensagem = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);

            } else if (tipo == "REEMBOLSO")
            {
                templateId = "d-b4d88f865caa47c0b50aef307dee9965";
                mensagem = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);

            } else if (tipo == "NORMAL")
            {
                templateId = "d-17ba8c4a0ab74e7b8c3ff3016b897d43";
                mensagem = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);
            }

            await client.SendEmailAsync(mensagem);
        }

    }
}
