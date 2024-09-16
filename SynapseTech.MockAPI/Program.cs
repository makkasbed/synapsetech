using Newtonsoft.Json.Linq;
using SynapseTech.MockAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/orders", () =>
{
    var data = File.ReadAllText("orders.json");

    JToken jToken = JToken.Parse(data);
    string formattedJson = jToken.ToString(Newtonsoft.Json.Formatting.None);
    

    return Results.Ok(formattedJson);
});

app.MapPost("/alerts", (AlertData data) =>{

    return Results.Ok(data);
});

app.Run();