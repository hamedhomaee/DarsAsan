namespace DarsAsan.Utilities
{
    public interface IMailSender
    {
        public void SendMail(string From, string To, string SmtpServer, int SmtpPort, string MailBody, string Subject, bool IsBodyHtml, string SenderEmail, string SenderPassword, bool IsSSLEnabled);
    }
}