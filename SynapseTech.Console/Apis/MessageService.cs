using Microsoft.Extensions.Configuration;
using Serilog;

using SynapseTech.Console.Models;
using Newtonsoft.Json.Linq;

namespace SynapseTech.Console.Apis;

public class MessageService
{


    public static bool SendAlertMessage(Order order, string alertApiUrl)
    {
        bool status = false;

        using (var client = new HttpClient())
        {
            //fetch API url
            var alertData = new
            {

            };

            //convert the data to acceptable for api call 
            var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");
            //make api call
            var response = client.PostAsync(alertApiUrl, content).Result;

            //check api call is successful
            if (response.IsSuccessStatusCode)
            {
                //log the success information to the file
                Log.Logger.Information($"Alert sent for delivered order {order.OrderId}");
                status = true;
            }
            else
            {
                //log the failed status to the log file.
                Log.Logger.Information($"Failed to send alert for delivered order: {order.OrderId}");
            }
        }
        return status;

    }
}