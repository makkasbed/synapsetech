using System.Net.Http.Headers;
using SynapseTech.Console.Models;

namespace SynapseTech.Console;

public interface IMessageService {
    public bool SendAlertMessage(Order order);
}