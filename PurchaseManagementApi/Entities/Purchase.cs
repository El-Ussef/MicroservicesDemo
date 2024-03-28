using PurchaseManagementApi.Contracts;
using PurchaseManagementApi.Contracts.Requests;
using PurchaseManagementApi.Events;
using PurchaseManagementApi.Heplers;

namespace PurchaseManagementApi.Entities;

public class Purchase : IHasEvent
{
    private Status _status;
    
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Created { get; protected set; }

    public int QuantityPurchased { get; protected set;} = 0;


    public Status Status
    {
        get => _status;
    }

    public Guid ProductId { get; protected set;}
    
    //public Product Product { get; set; } = null;
    
    public List<IEvent> Events { get; set; } =new();

    protected Purchase()
    {
        
    }
    public Purchase(int quantity, Guid productId)
    {
        Created = DateTime.Now;
        ProductId = productId;
        QuantityPurchased = quantity;
        _status = Status.Created;
        Events.Add(new PurchaseCreatedEvent()
        {
            ProductId = ProductId,
            Quantity = QuantityPurchased
        });
    }

    public void Validate()
    {
        _status = Status.Validated;
        //TODO:add Validated Event and publshied to Rmq
        Events.Add(new PurchasedValidatedEvent
        {
            ProductId = ProductId,
            Quantity = QuantityPurchased
        });
    }
    
    public void Canceled()
    {
        _status = Status.Canceled;
        //TODO:add Canceled Event and publshied to Rmq
    }

}