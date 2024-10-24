using System.Text;

namespace DataFaker.Domain.Email;

public class MailgunClient : HttpClient
{
    private readonly string _apiKey;
    private readonly string _domain;

    public MailgunClient(string apiKey, string domain)
    {
        _apiKey = apiKey;
        _domain = domain;
        BaseAddress = new Uri($"https://api.mailgun.net/v3/{_domain}/");
        DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_apiKey}")));
    }

    public async Task<string> SendEmailAsync(string from, string to, string subject, string text)
    {
        var content = new MultipartFormDataContent
        {
            { new StringContent(from), "from" },
            { new StringContent(to), "to" },
            { new StringContent(subject), "subject" },
            { new StringContent(text), "text" }
        };

        HttpResponseMessage response = await PostAsync("messages", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception($"Error sending email: {response.ReasonPhrase}");
        }
    }
}
