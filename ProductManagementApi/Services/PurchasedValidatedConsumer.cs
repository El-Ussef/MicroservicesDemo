using MassTransit;
using MessagingContract.PurchaseApi;
using MicroserviceDemo.Contracts.Products;
using MicroserviceDemo.Contracts.Requests;

namespace MicroserviceDemo.Services;

public class PurchasedValidatedConsumer : IConsumer<PurchaseValidated>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<PurchasedValidatedConsumer> _logger;

    public PurchasedValidatedConsumer(IProductRepository productRepository,ILogger<PurchasedValidatedConsumer> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<PurchaseValidated> context)
    {
        var message = context.Message;
        _logger.LogInformation("Product id : {0}",message.productId.ToString());
         _productRepository.Update(message.productId, new UpdateProductRequest() { Quantity = message.quantity });
         return Task.CompletedTask;
    }
}