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


    public ProcessService(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;

    }

    /// <summary>
    /// Checks if an order has been delivered
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public bool IsDelivered(Order order)
    {
        _logger.Information($"Checking order {order.OrderId} status: {order.Status}");
        return order.Status==OrderStatus.Delivered.ToString();
    }
    /// <summary>
    /// Processes an order and return the status
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public ProcessStatus ProcessOrder(Order order)
    {
        _logger.Information($"Processing order: {order.OrderId}");
        //check if order is delivered
        if (IsDelivered(order))
        {

            //retrieve alert url
            var alertApiUrl = _configuration["AlertAPI"];
            //send alert message
            bool status = MessageService.SendAlertMessage(order, alertApiUrl!);
            if (status)
            {
                return ProcessStatus.SUCCESSFUL;
            }
            else
            {
                return ProcessStatus.FAILED;
            }
        }
        else
        {
            return ProcessStatus.PENDING;
        }
    }
}