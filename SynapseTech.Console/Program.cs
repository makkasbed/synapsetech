// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Serilog;
using SynapseTech.Console.Apis;

using SynapseTech.Console.Models;

namespace SynapseTech.Console;

public class Program
{

    public static void Main(string[] args)
    {

        // build configuration based on appsettings
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        //configure serilog for logging
        Log.Logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(configuration)
                     .CreateLogger();

        try
        {
            Log.Information("Start of App");
            ProcessOrders(configuration, Log.Logger);
            Log.Information("Processing ended");

        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occured");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
    public async static void ProcessOrders(IConfiguration configuration, ILogger logger)
    {

        //start the fetch the orders
        FetchService fetchService = new FetchService(configuration, logger);
        List<Order> orders = await fetchService.FetchOrdersAsync();
        //pass the orders to the process service
        ProcessService processService = new ProcessService(configuration, Log.Logger);
        foreach (Order order in orders)
        {
            Log.Information($"Processing order {order.OrderId}");
            processService.ProcessOrder(order);
        }
    }


}
