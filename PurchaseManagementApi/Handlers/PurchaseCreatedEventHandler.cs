using MassTransit;
using MediatR;
using MessagingContract.PurchaseApi;
using PurchaseManagementApi.Events;

namespace PurchaseManagementApi.Handlers;

public class PurchaseCreatedEventHandler: INotificationHandler<PurchaseCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBus _bus;
    private readonly ILogger<PurchaseCreatedEventHandler> _logger;

    public PurchaseCreatedEventHandler(IPublishEndpoint publishEndpoint,
        IBus bus,
        ILogger<PurchaseCreatedEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _bus = bus;
        _logger = logger;
    }
    public async Task Handle(PurchaseCreatedEvent notification, CancellationToken cancellationToken)
    {
        //TODO: publish the message to RMQ
        var purchaseCreated = new PurchaseCreated(notification.ProductId,notification.Quantity);
        _logger.LogInformation("PurchaseCreated : {0}",purchaseCreated);
        await _publishEndpoint.Publish(purchaseCreated,cancellationToken).ConfigureAwait(false);
        //await _bus.Publish(purchaseCreated,cancellationToken).ConfigureAwait(false);
        //TODO:add logger here
    }
}