using Microsoft.AspNetCore.Mvc;
using PurchaseManagementApi.Contracts.IRepositories;
using PurchaseManagementApi.Contracts.Requests;
using PurchaseManagementApi.ExternalServices.Contracts;

namespace PurchaseManagementApi.Controllers;

[Route("api/Purchases")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductsApi _productsApi;

    public PurchasesController(IPurchaseRepository purchaseRepository,IProductsApi productsApi)
    {
        _purchaseRepository = purchaseRepository;
        _productsApi = productsApi;
    }
    
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productsApi.GetAppProducts();
        return Ok(products);
    }
    
    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var tempPurchase = await _purchaseRepository.GetAll();
        return Ok(tempPurchase);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var tempPurchase = await _purchaseRepository.Get(id);
        if ( tempPurchase is null)
        {
            return BadRequest(tempPurchase);
        }
        return Ok(tempPurchase);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreatePurchaseRequest createPurchaseRequest)
    {
        var created = await _purchaseRepository.Create(createPurchaseRequest);
        if (!created)
        {
            return BadRequest(created);
        }
        return Ok(created);
    }
    
    [HttpPut]
    public async Task<IActionResult> Validate(Guid id)
    {
        var validated = await _purchaseRepository.Validate(id);
        if (!validated)
        {
            return BadRequest(validated);
        }
        return Ok(validated);
    }
}