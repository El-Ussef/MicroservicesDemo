using PurchaseManagementApi.Contracts.Requests;

namespace PurchaseManagementApi.Events;

public class PurchaseCreatedEvent  : IEvent
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}