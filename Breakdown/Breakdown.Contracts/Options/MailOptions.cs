using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.Options
{
    public class MailOptions
    {
        public string SmtpUrl { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
    }
}
