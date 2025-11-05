using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Service
{
    public class EmailSettings 
    {
        private readonly IConfiguration _configuration;

        public EmailSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string SmtpServer => _configuration.GetSection("EmailSettings")["SmtpServer"];
        private int SmtpPort => int.Parse( _configuration.GetSection("EmailSettings")["SmtpPort"]);
        private string SenderEmail => _configuration.GetSection("EmailSettings")["SenderEmail"];
        private string SenderToken => _configuration.GetSection("EmailSettings")["senderToken"];
        public async Task SendEmailAsync(string Receivers, string Subject, string Body)
        {

            using SmtpClient client = new SmtpClient(SmtpServer, SmtpPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SenderEmail, SenderToken),
            };


            using MailMessage Message = new MailMessage(SenderEmail, Receivers, Subject, Body)
            {
                IsBodyHtml = true
            };
            
             await client.SendMailAsync(Message);
        }
    }
}
