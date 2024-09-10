using Api.Application.Request;
using Api.Application.Response;
using Api.Domain;
using Api.Infrastructure.Repository;

namespace Api.Application.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

    /// <summary>
    /// Retrieves all products from the repository.
    /// </summary>
    /// <returns>A list of <see cref="GetProductResponse"/> objects representing the products.</returns>
    public async Task<IEnumerable<GetProductResponse>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(p => new GetProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price
        });
    }

    /// <summary>
    /// Retrieves a product by its ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A <see cref="GetProductByIdResponse"/> object representing the product.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the product with the specified ID does not exist.</exception>
    public async Task<GetProductByIdResponse> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return new GetProductByIdResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    /// <summary>
    /// Adds a new product to the repository.
    /// </summary>
    /// <param name="request">The request containing the details of the product to add.</param>
    /// <returns>A <see cref="CreateProductResponse"/> object representing the created product.</returns>
    public async Task<CreateProductResponse> AddAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        var createdProduct = await _productRepository.AddAsync(product);

        return new CreateProductResponse
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price
        };
    }

    /// <summary>
    /// Updates an existing product in the repository.
    /// </summary>
    /// <param name="request">The request containing the updated details of the product.</param>
    /// <returns>A <see cref="UpdateProductResponse"/> object representing the updated product.</returns>
    public async Task<UpdateProductResponse> UpdateAsync(UpdateProductRequest request)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price
        };

        await _productRepository.UpdateAsync(product);

        return new UpdateProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    /// <summary>
    /// Deletes a product from the repository by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the product to delete.</param>
    public async Task DeleteAsync(DeleteProductRequest request)
    {
        await _productRepository.DeleteAsync(request.Id);
    }
}