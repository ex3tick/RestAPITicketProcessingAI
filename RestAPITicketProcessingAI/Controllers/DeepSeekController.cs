using Microsoft.AspNetCore.Mvc;
using RestAPITicketProcessingAI.Models;
using RestAPITicketProcessingAI.Service;

namespace RestAPITicketProcessingAI.Controllers;

public class DeepSeekController : Controller
{
    private readonly DeepSeekControllerService _dCService = new DeepSeekControllerService();
    [HttpGet]
    [Route("api/deepseek")]
    public async Task<IActionResult> GetTicketAnalyseAsync([FromBody]TicketEntity ticket)
    {
      var response = await _dCService.GetTicketAnalyseAsync(ticket);
        return Ok(response);
    }
}