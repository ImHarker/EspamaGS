using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace EspamaGS_2._0.Services {
    public class EmailSender : IEmailSender {

        // Our private configuration variables
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _userName;
        private string _password;

        // Get our parameterized configuration
        public EmailSender(string host, int port, bool enableSSL, string userName, string password) {
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _userName = userName;
            _password = password;
        }

        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage) {


            var client = new SmtpClient(_host, _port) {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL
            };
            return client.SendMailAsync(
                new MailMessage(_userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }
}
