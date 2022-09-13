using DEMAT.Models.Sendgrid;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Sendgrid
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings _emailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmail(Email email)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("gatewayjustcode@gmail.com", "DEMAT");
            var subject = email.Subject;
            var to = new EmailAddress(email.To , email.To);
     
            var message = MailHelper.CreateSingleEmail(from, to, subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;

        }
    }
}
