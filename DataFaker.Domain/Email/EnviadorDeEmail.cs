using System.Net;
using System.Net.Mail;

namespace DataFaker.Domain.Email;

public interface IEmailService
{
    void EnviarEmailAsync(string destinatario, string assunto, string corpo, Anexo anexo);
}

public class EmailService : IEmailService
{
    public void EnviarEmailAsync(string destinatario, string assunto, string corpo, Anexo anexo)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("");
        mailMessage.To.Add(destinatario);
        mailMessage.Subject = "Hello world";
        mailMessage.Body = "This is a test email sent using C#.Net";

        var smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("", "");
        smtpClient.EnableSsl = true;

        smtpClient.Send(mailMessage);
    }
}