using Newtonsoft.Json.Linq;
using SynapseTech.MockAPI;
using SynapseTech.MockAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/orders", () =>
{

    List<Order> orders = new List<Order>();
    orders.Add(new Order
    {
        OrderId = "12345",
        Description = "Test 1",
        Status = "Delivered",
        deliveryNotification= 0,
        Items = new List<Item> {
            new Item{
                Name= "Test1",
                ItemId = "54321",
                Status= "Delivered",
                Description = "Test 1",
            }
        }
    });

    return Results.Ok(orders);
});


app.MapPost("/alerts", (AlertData data) =>
{

    return Results.Ok(data);
});

app.Run();