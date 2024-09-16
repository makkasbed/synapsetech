using Microsoft.Extensions.Configuration;
using Serilog;

using SynapseTech.Console.Models;
using Newtonsoft.Json.Linq;

namespace SynapseTech.Console.Apis;

public class MessageService
{

    /// <summary>
    /// This function gets a delivered order and sends it to the Update API.
    /// </summary>
    /// <param name="order"></param>
    /// <param name="alertApiUrl"></param>
    /// <returns></returns>
    public static bool SendAlertMessage(Order order, string alertApiUrl)
    {
        bool status = false;

        using (var client = new HttpClient())
        {
            //prepare message
            string message = FormatMessage(order);
            //preprare alert data for alerts api
            var alertData = new
            {
                Message = message,
            };

            //convert the data to acceptable form for api call 
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
    private static string FormatMessage(Order order){
        string message = string.Empty;

        message = $"Alert for delivered order: Order {order.OrderId}: Items:";
        foreach(var item in order.Items){
            message += item.Description +"\n";
        }
        message += $"Delivery Notification: {order.deliveryNotification+1}";

        return message;
    }
}