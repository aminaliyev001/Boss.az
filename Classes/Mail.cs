using System.Net;
using System.Net.Mail;
namespace MailNameSpace;
public static class MailSender
{
    private static string _smtpServer = "smtp.gmail.com";
    private static int _smtpPort = 587;
    private static string _fromEmail = "rideparkuber@gmail.com";
    private static string _password = "ajbbjkamilfrcbau";
    public static bool SendMail(string toEmail, string subject, string body)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(_fromEmail, _password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}