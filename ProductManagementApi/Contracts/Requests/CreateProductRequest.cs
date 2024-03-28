namespace MicroserviceDemo.Contracts.Requests;

public class CreateProductRequest
{
    public required string Label { get; set; }

    public int Quantity { get; set; }
}