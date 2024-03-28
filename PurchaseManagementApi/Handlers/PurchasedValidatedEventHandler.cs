using MassTransit;
using MediatR;
using MessagingContract.PurchaseApi;
using PurchaseManagementApi.Events;

namespace PurchaseManagementApi.Handlers;

public class PurchasedValidatedEventHandler : INotificationHandler<PurchasedValidatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBus _bus;
    private readonly ILogger<PurchasedValidatedEventHandler> _logger;

    public PurchasedValidatedEventHandler(IPublishEndpoint publishEndpoint,
        IBus bus,
        ILogger<PurchasedValidatedEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _bus = bus;
        _logger = logger;
    }
    public async Task Handle(PurchasedValidatedEvent notification, CancellationToken cancellationToken)
    {
        //TODO: publish the message to RMQ
        var purchaseValidated = new PurchaseValidated(notification.ProductId,notification.Quantity);
        //await _publishEndpoint.Publish(purchaseValidated,cancellationToken).ConfigureAwait(false);
        _logger.LogInformation("purchaseValidated : {0}",purchaseValidated);
        await _bus.Publish(purchaseValidated,cancellationToken).ConfigureAwait(false);
        //TODO:add logger here
    }
}