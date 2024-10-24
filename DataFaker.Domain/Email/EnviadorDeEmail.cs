namespace DataFaker.Domain.Email;

using DataFaker.Domain.Componentes;
using System.Threading.Tasks;

public interface IEmailService
{
    Task EnviarEmailAsync(string destinatario, string assunto, string corpo, Anexo anexo);
}

public class EmailService : IEmailService
{
    public async Task EnviarEmailAsync(string destinatario, string assunto, string corpo, Anexo anexo)
    {
        var client = new MailgunClient(App.MailgunToken, App.MailgunDomain);
        var response = await client.SendEmailAsync(
            "Excited User <mailgun@sandbox32bb813a8cc5402c86976e2b857a68d2.mailgun.org>",
            "pedrohm1009@gmail.com",
            "Hello",
            "Testing some Mailgun awesomeness!"
        );
    }
}

