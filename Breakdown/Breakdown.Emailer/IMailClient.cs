using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Emailer
{
    public interface IMailClient
    {
        void Send(string subject, string body, List<string> to);
    }
}
