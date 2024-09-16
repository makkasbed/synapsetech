namespace SynapseTech.MockAPI;

public class Order {
    public string? OrderId { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
     public int deliveryNotification { get; set; }

    public List<Item> Items { get; set; } = new List<Item>();
}