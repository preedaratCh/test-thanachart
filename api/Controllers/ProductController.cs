using api.Models.Dtos;
using api.Models.Requests;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById([FromRoute] Guid id)
    {
        var product = await _productService.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var product = await _productService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update([FromRoute] Guid id, [FromBody] ProductRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedProduct = await _productService.Update(id, request);
        if (updatedProduct == null)
        {
            return NotFound();
        }
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var existingProduct = await _productService.GetById(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _productService.Delete(id);
        return NoContent();
    }
}