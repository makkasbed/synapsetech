using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using SynapseTech.Console.Contracts;
using SynapseTech.Console.Models;
using Serilog;

namespace SynapseTech.Console.Apis;

public class FetchService : IFetchService
{
    private IConfiguration _configuration;
    private ILogger _logger;


    public FetchService(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// This function connects to the API and fetches all orders available
    /// </summary>
    /// <returns></returns>
    public async Task<List<Order>> FetchOrdersAsync()
    {
        //create new list of orders
        List<Order> orders = new List<Order>();
        //get the fetch API url
        var FetchUrl = _configuration["FetchAPI"];

        _logger.Information("Fetching orders");
        using (var client = new HttpClient())
        {
            //make a get call to the api
            var response = await client.GetAsync(FetchUrl);
            //check if the status code is successful
            if (response.IsSuccessStatusCode)
            {
                //get the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                //convert the response to list of order objects
                orders = JsonConvert.DeserializeObject<List<Order>>(responseBody)!;
            }
            else
            {
                _logger.Error("Failed to fetch orders from API.");
                orders = new List<Order>();
            }
        }
        return orders;
    }
}