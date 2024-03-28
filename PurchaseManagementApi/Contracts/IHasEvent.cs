using PurchaseManagementApi.Contracts.Requests;

namespace PurchaseManagementApi.Contracts;

public interface IHasEvent
{
    public List<IEvent> Events { get; set; }
}