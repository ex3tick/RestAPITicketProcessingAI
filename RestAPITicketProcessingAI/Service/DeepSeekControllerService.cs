using RestAPITicketProcessingAI.Models;
using RestAPITicketProcessingAI.Models.DeepSeekLogic;

namespace RestAPITicketProcessingAI.Service;

public class DeepSeekControllerService
{
    private readonly DeepSeekConnection _dCon = new DeepSeekConnection();
    public async Task<ResponseModel> GetTicketAnalyseAsync(TicketEntity ticket)
    {
        return await GetResponseModelAsync(ticket);
    }

    private async Task<ResponseModel> GetResponseModelAsync(TicketEntity ticket)
    {
        var returnModel = new ResponseModel();
        var response = await _dCon.SendRequstAsync(ticket);
        string[] responseArray = response.Split(',');

        returnModel.Department = responseArray[0];
        returnModel.Lanugage = responseArray[1];
        returnModel.DevLevel = int.Parse(responseArray[2]);
        
        
        return returnModel;
    }
}