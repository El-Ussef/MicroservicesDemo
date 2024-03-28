using PurchaseManagementApi.Contracts.Requests;
using PurchaseManagementApi.Entities;

namespace PurchaseManagementApi.Contracts.IRepositories;

public interface IPurchaseRepository
{

    Task<IEnumerable<Purchase>>? GetAll();
    
    Task<Purchase?> Get(Guid id);
    
    Task<bool> Create(CreatePurchaseRequest createPurchaseRequest);
    
    Task<bool> Validate(Guid id);

    
    //Task<bool> Update(Guid id, UpdateProductRequest product);
}