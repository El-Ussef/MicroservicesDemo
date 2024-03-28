namespace PurchaseManagementApi.Entities;

public class Product
{
    public Guid Id { get; set; }

    public required string Label { get; set; }
}