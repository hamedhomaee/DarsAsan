using DarsAsan.MailSender;

namespace DarsAsan.Utilities
{
    public class MainMailSender : IMailSender
    {
        public void SendMail(string From, string To, string SmtpServer, int SmtpPort, string MailBody, string Subject, bool IsBodyHtml, string SenderEmail, string SenderPassword, bool IsSSLEnabled)
        {
            MailSender.Program.SendMail(From, To, SmtpServer, SmtpPort, MailBody, Subject, IsBodyHtml, SenderEmail, SenderPassword, IsSSLEnabled);
        }
    }
}