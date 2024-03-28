namespace MicroserviceDemo.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Label { get; set; }

    public int Quantity { get; set; }
    
    
}