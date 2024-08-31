using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MailerSendEmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;

    public MailerSendEmailService(string apiToken)
    {
        _httpClient = new HttpClient();
        _apiToken = apiToken;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.mailersend.com/v1/email");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

        var payload = new
        {
            from = new { email = "project@XD.mlsender.net", name = "Password reminder" },
            to = new[] { new { email = to } },
            subject = subject,
            html = body
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to send email: {content}");
        }
    }
}