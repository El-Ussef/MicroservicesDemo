using MicroserviceDemo.Contracts.Requests;
using MicroserviceDemo.Entities;

namespace MicroserviceDemo.Contracts.Products;

public interface IProductRepository
{
    Task<Product?> Get(Guid id);
    
    Task<IEnumerable<Product>> GetAll();
    
    Task<bool> Create(Product product);
    
    Task<bool> Update(Guid id, UpdateProductRequest product);
}