using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AuraCommerce.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string fromName, string fromEmail, string subject, string messageBody)
        {
            var host = _configuration["EmailSettings:Host"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);
            var userName = _configuration["EmailSettings:UserName"];
            var password = _configuration["EmailSettings:Password"].Replace(" ", "");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("AuraCommerce Site", userName));
            email.To.Add(new MailboxAddress("Admin", userName));
            email.Subject = $"[Contato] {subject}";

            // Adiciona o Reply-To para o e-mail do cliente
            email.ReplyTo.Add(new MailboxAddress(fromName, fromEmail));

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h3>Nova mensagem de contato</h3>" +
                           $"<p><strong>Nome:</strong> {fromName}</p>" +
                           $"<p><strong>E-mail:</strong> {fromEmail}</p>" +
                           $"<p><strong>Mensagem:</strong> {messageBody}</p>"
            };

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                // Conecta usando StartTls (Porta 587)
                await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);

                // Autentica com a conta Gmail e Senha de App
                await smtp.AuthenticateAsync(userName, password);

                // Envia o e-mail
                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}