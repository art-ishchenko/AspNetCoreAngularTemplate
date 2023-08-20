using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AspNetCoreAngularTemplate.Application.Common.Interfaces;
using AspNetCoreAngularTemplate.Application.Common.Models;
using AspNetCoreAngularTemplate.Infrastructure.Logging;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AspNetCoreAngularTemplate.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _config;
        private readonly ILogger _logger;

        public EmailSender(IOptions<EmailSenderOptions> config, ILogger<EmailSender> logger)
        {
            _config = config.Value;
            _logger = logger;
        }
        
        public async Task<Result> SendEmailAsync(
            string recipientEmail,
            string subject,
            string body,
            bool isHtml = true)
        {
            var from = new MailboxAddress(_config.Name, _config.EmailAddress);
            var to = new MailboxAddress(recipientEmail, recipientEmail);
            return await SendEmailAsync(from, new[] { to }, subject, body, isHtml);
        }

        public async Task<Result> SendEmailAsync(
            string recipientName,
            string recipientEmail,
            string subject,
            string body,
            bool isHtml = true)
        {
            var from = new MailboxAddress(_config.Name, _config.EmailAddress);
            var to = new MailboxAddress(recipientName, recipientEmail);

            return await SendEmailAsync(from, new[] { to }, subject, body, isHtml);
        }

        public async Task<Result> SendEmailAsync(
            string senderName,
            string senderEmail,
            string recipientName,
            string recipientEmail,
            string subject,
            string body,
            bool isHtml = true)
        {
            var from = new MailboxAddress(senderName, senderEmail);
            var to = new MailboxAddress(recipientName, recipientEmail);

            return await SendEmailAsync(from, new[] { to }, subject, body, isHtml);
        }

        private async Task<Result> SendEmailAsync(
            MailboxAddress sender,
            MailboxAddress[] recipients,
            string subject,
            string body,
            bool isHtml = true)
        {
            var message = new MimeMessage();
            message.From.Add(sender);
            message.To.AddRange(recipients);
            message.Subject = subject;
            message.Body = isHtml ? new BodyBuilder { HtmlBody = body }.ToMessageBody() : new TextPart("plain") { Text = body };

            try
            {
                using (var client = new SmtpClient())
                {
                    if (!this._config.UseSsl)
                        client.ServerCertificateValidationCallback = (object sender2, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors) => true;

                    await client.ConnectAsync(this._config.Host, this._config.Port, this._config.UseSsl).ConfigureAwait(false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (!string.IsNullOrWhiteSpace(this._config.Username))
                        await client.AuthenticateAsync(this._config.Username, this._config.Password).ConfigureAwait(false);

                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.SendEmail, ex, "An error occurred whilst sending email");
                return Result.Failure(ex.Message);
            }
        }
    }
}
