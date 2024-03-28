using PurchaseManagementApi.Entities;

namespace PurchaseManagementApi.ExternalServices.Contracts;

public interface IProductsApi
{
    Task<List<Product>> GetAppProducts();
}