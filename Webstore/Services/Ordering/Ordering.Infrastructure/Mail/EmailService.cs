﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            // Kada ubrizgavamo nesto sto smo napravili sa nasim options, mora da se koristi IOptions
            _mailSettings = mailSettings.Value ?? throw new ArgumentNullException(nameof(mailSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(Email emailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            // AddRange za vise korisnika slanje
            email.Subject = emailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = emailRequest.Body;
            builder.TextBody = emailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            try
            {
                _logger.LogInformation("Sending email via SMTP server {serverName}", _mailSettings.Host);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occured when sending email via SMTP server {_mailSettings.Host} : {ex.Message}");
                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }

            return true;

        }
    }
}
// znaci Infrastructure je napravljen da implementira sve servise
// iz Application sloja