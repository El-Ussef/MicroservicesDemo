using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.Contracts.IRepositories;
using PurchaseManagementApi.Contracts.Requests;
using PurchaseManagementApi.DataBase;
using PurchaseManagementApi.Entities;
using PurchaseManagementApi.Heplers;

namespace PurchaseManagementApi.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly AppDbContext _dbContext;

    public PurchaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Purchase>>? GetAll()
    {
        return await _dbContext.Purchases.ToListAsync();

    }
    
    public async Task<Purchase?> Get(Guid id)
    {
        return await _dbContext.Purchases.FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<bool> Create(CreatePurchaseRequest createPurchaseRequest)
    {
        var purchase = new Purchase(createPurchaseRequest.QuantityPurchased, createPurchaseRequest.ProductId);
        
        await _dbContext.Purchases.AddAsync(purchase);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Validate(Guid id)
    {
        var tempPurchase = await _dbContext.Purchases.FirstOrDefaultAsync(p => p.Id == id);
        tempPurchase?.Validate();
        _dbContext.Purchases.Update(tempPurchase);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}