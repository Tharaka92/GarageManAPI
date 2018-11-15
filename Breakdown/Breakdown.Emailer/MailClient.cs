using Breakdown.Contracts.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Breakdown.Emailer
{
    public class MailClient : IMailClient
    {
        private readonly MailOptions _mailOptions;
        public MailClient(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
        }

        public async void Send(string subject, string body, List<string> to)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_mailOptions.SenderEmail, _mailOptions.SenderEmail)
                };

                foreach (string address in to)
                {
                    mail.To.Add(new MailAddress(address));
                }

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_mailOptions.SmtpUrl, _mailOptions.Port))
                {
                    smtp.Credentials = new NetworkCredential(_mailOptions.SenderEmail, _mailOptions.SenderPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
