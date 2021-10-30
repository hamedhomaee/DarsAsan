using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DarsAsan.MailSender
{
    public class Program
    {
        public static void SendMail(string From, string To, string SmtpServer, int SmtpPort, string MailBody, string Subject, bool IsBodyHtml, string SenderPassword, bool IsSSLEnabled)
        {
            MailMessage TheMessage = new MailMessage(From, To);

            TheMessage.Body = MailBody;
            TheMessage.BodyEncoding = Encoding.UTF8;
            TheMessage.IsBodyHtml = IsBodyHtml;
            TheMessage.Subject = Subject;

            SmtpClient client = new SmtpClient(SmtpServer, SmtpPort);

            NetworkCredential BasicCredential = new NetworkCredential(From, SenderPassword);

            client.EnableSsl = IsSSLEnabled;

            client.UseDefaultCredentials = false;
            client.Credentials = BasicCredential;

            client.Send(TheMessage);
        }
    }
}