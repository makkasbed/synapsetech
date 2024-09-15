using SynapseTech.Console.Models;

namespace SynapseTech.Console.Contracts;

public interface IFetchService {
    public Task<List<Order>> FetchOrdersAsync();
}