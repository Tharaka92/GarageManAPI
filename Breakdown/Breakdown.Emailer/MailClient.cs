using Breakdown.Contracts.DTOs;
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
        private readonly MailSettingDto _mailSettings;
        public MailClient(IOptions<MailSettingDto> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async void Send(string subject, string body, List<string> to)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderEmail)
                };

                foreach (string address in to)
                {
                    mail.To.Add(new MailAddress(address));
                }

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_mailSettings.SmtpUrl, _mailSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.SenderPassword);
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
