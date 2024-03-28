using MicroserviceDemo.Contracts.Products;
using MicroserviceDemo.Contracts.Requests;
using MicroserviceDemo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDemo.Controllers;

[Route("api/Products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var product = await _repository.GetAll();
        return Ok(product);
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _repository.Get(id);
        
        if (product is null)
        {
            return BadRequest();
        }
        
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(CreateProductRequest productRequest)
    {
        var product = new Product
        {
            Label = productRequest.Label,
            Quantity = productRequest.Quantity
        };
            
        var created = await _repository.Create(product);
        
        if (!created)
        {
            return BadRequest(created);
        }
        
        return Ok(created);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid id, UpdateProductRequest productUpdate)
    {

        var updated = await _repository.Update(id,productUpdate);
        
        if (!updated)
        {
            return BadRequest(updated);
        }
        
        return Ok(updated);
    }
    
}