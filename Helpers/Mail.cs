using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FindYourCarMechanic
{
    public class Mail
    {
        private readonly MailAddress from;
        private readonly SmtpClient smtp;

        public Mail(IConfiguration configuration)
        {
            from = new MailAddress(configuration.GetValue<string>("MailSettings:Mail"));
            smtp = new SmtpClient();
            smtp.Host = configuration.GetValue<string>("MailSettings:Host");
            smtp.Port = configuration.GetValue<int>("MailSettings:Port");
            smtp.Credentials = new NetworkCredential(
               configuration.GetValue<string>("MailSettings:Mail"),
               configuration.GetValue<string>("MailSettings:Password")
               );
            smtp.EnableSsl = true;

        }


        public void SendEmail(string toMailAddress, ConfirmedRepair repair) 
        {
            MailAddress to = new MailAddress(toMailAddress);

            MailMessage mail = new MailMessage(from, to);

            mail.Subject = "Car mechanic visit reminder.";

            mail.Body = $"You have a visit on {repair.StartDate.ToString(("MM/dd/yyyy HH:mm"))} in the workshop {repair.Mechanic.Name}.";

            smtp.Send(mail);
        }
    }
}
