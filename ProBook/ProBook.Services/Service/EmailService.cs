using Microsoft.Extensions.Configuration;
using ProBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Service
{
    public class EmailService:IEmailService
    {

       

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            string smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "smtp.gmail.com";
            int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
            string fromMail = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? "probookaplikacija@gmail.com";
            string password = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? "umfj dajr lmeb uzeg";
            var smtpClient = new SmtpClient()
            {
                Host = smtpServer,
                Port = smtpPort,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, password),
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromMail),
                Subject = subject,
                Body = body,
            };

            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
