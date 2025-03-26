using System.Text.Json.Serialization;

namespace RestAPITicketProcessingAI.Models;


public class TicketEntity
{
    
    [JsonPropertyName("ticketType")]
    public string TicketType { get; set; }
    
    [JsonPropertyName("ticketDescription")]
    public string TicketDescription { get; set; }
    
    [JsonPropertyName("ticketContent")]
    public string TicketContent { get; set; }
    
}