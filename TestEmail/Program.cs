using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var mail = new MailMessage("Alex.Webb@Simmons-Simmons.com", "Alex.Webb@Simmons-Simmons.com");
            mail.Subject = "Test email";
            mail.Body = "This is a test email...";

            SmtpClient client = new SmtpClient("smtp.simmons.local");
            client.UseDefaultCredentials = true;
            try
            {
                client.Send(mail);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
