namespace PurchaseManagementApi.Contracts.Requests;

public class CreatePurchaseRequest
{
    public Guid ProductId { get; set; }
    public int QuantityPurchased { get; set; } = 0;
}