using MassTransit;
using MessagingContract.PurchaseApi;
using MicroserviceDemo.Contracts.Products;
using MicroserviceDemo.Contracts.Requests;

namespace MicroserviceDemo.Services;

public class PurchaseCreatedConsumer : IConsumer<PurchaseCreated>
{
        private readonly IProductRepository _productRepository;
        private readonly ILogger<PurchaseCreatedConsumer> _logger;

        public PurchaseCreatedConsumer(IProductRepository productRepository,ILogger<PurchaseCreatedConsumer> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
    
        public Task Consume(ConsumeContext<PurchaseCreated> context)
        {
            var message = context.Message;
            _logger.LogInformation("Product id : {0}",message.productId.ToString());
            //_productRepository.Update(message.productId, new UpdateProductRequest() { Quantity = message.quantity });
            return Task.CompletedTask;
        }
}
