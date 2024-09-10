using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all products from the database.
    /// </summary>
    /// <returns>A list of <see cref="Product"/> entities.</returns>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Retrieves a product by its ID from the database.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The <see cref="Product"/> entity with the specified ID.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the product with the specified ID does not exist.</exception>
    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with Id {id} not found.");
        }

        return product;
    }

    /// <summary>
    /// Adds a new product to the database.
    /// </summary>
    /// <param name="product">The <see cref="Product"/> entity to add.</param>
    /// <returns>The added <see cref="Product"/> entity.</returns>
    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="product">The <see cref="Product"/> entity with updated details.</param>
    /// <exception cref="KeyNotFoundException">Thrown if the product with the specified ID does not exist.</exception>
    public async Task UpdateAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with Id {product.Id} not found.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a product from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if the product with the specified ID does not exist.</exception>
    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with Id {id} not found.");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}