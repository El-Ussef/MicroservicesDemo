using MicroserviceDemo.Contracts.Products;
using MicroserviceDemo.Contracts.Requests;
using MicroserviceDemo.DataBase;
using MicroserviceDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDemo.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Product?> Get(Guid id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<bool> Create(Product product)
    {
         await _dbContext.Products.AddAsync(product);

         return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Guid id, UpdateProductRequest product)
    {
        var tempProduct = await  _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (tempProduct is not null)
        {

            if (!string.IsNullOrWhiteSpace(product.Label))
            {
                tempProduct.Label = product.Label;
            }

            if (product.Quantity > 0)
            {
                tempProduct.Quantity = product.Quantity.Value;
            }
            
            _dbContext.Products.Update(tempProduct);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        return false;
    }
}