using SynapseTech.Console.Models;

namespace SynapseTech.Console.Contracts;

public interface IProcessService {
    public bool IsDelivered(Order order);
    public ProcessStatus ProcessOrder(Order order);
}