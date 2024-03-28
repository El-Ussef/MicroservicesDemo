namespace MicroserviceDemo.Contracts.Requests;

public class UpdateProductRequest
{
    public string? Label { get; set; }

    public int? Quantity { get; set; }
}