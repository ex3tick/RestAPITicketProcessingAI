using System.Text;
using System.Text.Json;
using RestAPITicketProcessingAI.Models.DeepSeekEntitys;

namespace RestAPITicketProcessingAI.Models.DeepSeekLogic;

public class DeepSeekConnection
{
    public static DeepSeekSettings deepSeekSettings { get; set; }


    public async Task<string> SendRequstAsync(TicketEntity ticket)
    {
        var requestData = new
        {
            model = deepSeekSettings.ModelName,
            messages = new[]
            {
                new { role = "system", content = deepSeekSettings.SystemRoleDefinition },
                new { role = "user", content = $"Ticket type is {ticket.TicketType} Ticket Decription is {ticket.TicketDescription} Ticket content is {ticket.TicketContent}" }
            },
            stream = false
        };
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {deepSeekSettings.ApiKey}");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestData),
                Encoding.UTF8,
                "application/json"
            );
            try
            {
                var response = await httpClient.PostAsync("https://api.deepseek.com/chat/completions", jsonContent);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<DeepSeekApiResponse>(responseBody);
                var content = apiResponse?.Choices?[0]?.Message?.Content;
                if (string.IsNullOrEmpty(content))
                {
                    throw new Exception("The 'content' in the API response is empty or null.");
                }

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
