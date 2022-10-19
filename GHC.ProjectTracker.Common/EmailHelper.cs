using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Common
{
    public class EmailHelper
    {
        public static void SendEmail(string to, string subject, string body)
        {
            MailMessage mail= new MailMessage();
            mail = new MailMessage(ConfigurationManager.AppSettings["SmtpFromEmail"], to);            
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings["SmtpHost"];
            string prefix = ConfigurationManager.AppSettings["SmtpSubjectPrefix"] ?? "";
            mail.Subject = prefix + subject;
            mail.Body = body;
            client.Send(mail);
        }
    }
}
