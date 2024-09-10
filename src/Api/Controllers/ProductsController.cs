using Api.Application.Request;
using Api.Application.Response;
using Api.Application.Service;
using Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A list of all <see cref="Product"/> objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetProductResponse>), 200)]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Retrieves a specific product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The <see cref="GetProductByIdResponse"/> for the specified product.</returns>
    /// <response code="200">Returns the product with the specified ID.</response>
    /// <response code="404">If the product with the specified ID is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProductByIdResponse), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The request object containing the details of the product to create.</param>
    /// <returns>The created <see cref="CreateProductResponse"/> object.</returns>
    /// <response code="201">The product was created successfully.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResponse), 201)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var createdProduct = await _productService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="request">The request object containing the updated details of the product.</param>
    /// <response code="204">The product was updated successfully.</response>
    /// <response code="404">If the product with the specified ID is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        request.Id = id;

        await _productService.UpdateAsync(request);
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <response code="204">The product was deleted successfully.</response>
    /// <response code="404">If the product with the specified ID is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(new DeleteProductRequest(id));
        return NoContent();
    }
}