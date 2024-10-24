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
        string from = Environment.GetEnvironmentVariable("SMTP_EMAIL") ?? "";
        string pass = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? "";

        if (from == "" || pass == "") return;

        var mailMessage = new MailMessage { From = new MailAddress(from) };
        mailMessage.To.Add(destinatario);
        mailMessage.Subject = assunto;
        mailMessage.Body = corpo;

        if (anexo != null && anexo.Stream != null && anexo.Stream.Length > 0)
        {
            var attachment = new Attachment(new MemoryStream(anexo.Stream), anexo.Nome, anexo.ContentType);
            mailMessage.Attachments.Add(attachment);
        }

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Host = "smtp.gmail.com",
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(from, pass),
            EnableSsl = true
        };

        smtpClient.Send(mailMessage);
    }
}