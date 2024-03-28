using System.Text.Json;
using Microsoft.Extensions.Options;
using PurchaseManagementApi.Entities;
using PurchaseManagementApi.ExternalServices.Contracts;
using PurchaseManagementApi.Heplers;

namespace PurchaseManagementApi.ExternalServices;

public class ProductsApi : IProductsApi
{
    private readonly IHttpClientFactory _httpClient;
    private readonly ProductApiOptions _options;

    public ProductsApi(IHttpClientFactory httpClient,
        IOptions<ProductApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<List<Product>> GetAppProducts()
    {
        try
        {
            var httpClient = _httpClient.CreateClient("productApi");

            httpClient.BaseAddress = new Uri(_options.BaseUrl);
            var response = await httpClient.GetAsync("/api/Products");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var productsVm =  await JsonSerializer.DeserializeAsync<List<ProductVM>>(contentStream);

                return productsVm.Select(p => new Product
                {
                    Id = p.Id,
                    Label = p.Label
                }).ToList();

            }

            Console.WriteLine($"Failed to retrieve product. Status code: {response.StatusCode}");
            return null;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}