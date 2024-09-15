using Microsoft.Extensions.Configuration;
using SynapseTech.Console.Contracts;
using SynapseTech.Console.Models;
using Serilog;
using SynapseTech.Console.Apis;

namespace SynapseTech.Console;

public class ProcessService : IProcessService
{
    private IConfiguration _configuration;
    private readonly ILogger _logger;
    

    public ProcessService(IConfiguration configuration,ILogger logger){
        _configuration = configuration;
        _logger = logger;
       
    }
    public bool IsDelivered(Order order)
    {
        return order.Status!.Equals(OrderStatus.Delivered);
    }

    public ProcessStatus ProcessOrder(Order order)
    {
        //check if order is delivered
        if(IsDelivered(order)){

           //retrieve alert url
           var alertApiUrl = _configuration["AlertAPI"]; 
            //send alert message
           bool status = MessageService.SendAlertMessage(order, alertApiUrl!);
           if(status){
             return ProcessStatus.SUCCESSFUL;
           }else {
            return ProcessStatus.FAILED;
           }
        }else {
            return ProcessStatus.PENDING;
        }
    }
}