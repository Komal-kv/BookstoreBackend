using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string Email, string token)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("testcoding29@gmail.com", "apcssvowvaoyscyh");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(Email);
                msgObj.From = new MailAddress("testcoding29@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = $"http://localhost:4200/resetPassword/{token}";
                smtpClient.Send(msgObj);
            }
        }
    }
}
