namespace SynapseTech.Test;
using SynapseTech.Console;
using SynapseTech.Console.Apis;
using SynapseTech.Console.Models;
using Moq;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using NUnit.Framework.Internal;

public class SynapseTechTests
{
    private Order? order;
    private const string LogFilePath = "test.log.txt";

    private Mock<IConfiguration> mockConfig;
    private Serilog.Core.Logger logger;

    private int ORDERS = 1;
    private string STATUS = "SUCCESSFUL";

    [SetUp]
    public void Setup()
    {
        mockConfig = new Mock<IConfiguration>();
        var logEvents = new List<LogEvent>();
        logger = new LoggerConfiguration().WriteTo.File(LogFilePath).CreateLogger();
        
        mockConfig.Setup(x => x["FetchAPI"]).Returns("http://localhost:5204/orders");
        mockConfig.Setup(x => x["AlertAPI"]).Returns("http://localhost:5204/alerts");
    }

    [Test]
    public async Task TestFetchOrders()
    {
        //arrange
        FetchService fetch = new FetchService(mockConfig.Object, logger);
        //act
        List<Order> orders = await fetch.FetchOrdersAsync();
        //assert
        Assert.That(ORDERS, Is.EqualTo(orders.Count));
        order = orders[0];
    }

    [Test]
    public void TestProcessOrder(){
        //arrange
        ProcessService processService = new ProcessService(mockConfig.Object,logger);
        //act
        ProcessStatus status = processService.ProcessOrder(order!);
        //assert
        Assert.That(STATUS, Is.EqualTo(status.ToString()));
    }
}