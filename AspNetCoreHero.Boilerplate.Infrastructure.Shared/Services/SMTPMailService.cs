using AspNetCoreHero.Boilerplate.Application.DTOs.Mail;
using AspNetCoreHero.Boilerplate.Application.DTOs.Settings;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Services
{
    public class SMTPMailService : IMailService
    {
        public MailSettings _settings { get; }
        public ILogger<SMTPMailService> _logger { get; }

        public SMTPMailService(IOptions<MailSettings> mailSettings, ILogger<SMTPMailService> logger)
        {
            _settings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? _settings.From);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                //using var smtp = new SmtpClient();
                //smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                //smtp.Authenticate(_settings.UserName, _settings.Password);
                //await smtp.SendAsync(email);
                //smtp.Disconnect(true);

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, cancellationToken);
                await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, cancellationToken);
                await smtp.SendAsync(email, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}