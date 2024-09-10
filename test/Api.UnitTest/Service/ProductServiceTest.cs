using Api.Application.Service;
using Api.Domain;
using Api.Infrastructure.Repository;
using Api.UnitTest.Helper;
using Moq;

namespace Api.UnitTest.Service;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsProductList()
    {
        // Arrange
        var products = new List<Product>
        {
            TestData.SampleProduct,
            TestData.AnotherSampleProduct
        };
        _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

        // Act
        var result = await _productService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(TestData.SampleProduct.Name, result.First().Name);
    }

    [Fact]
    public async Task GetByIdAsync_ProductExists_ReturnsProduct()
    {
        // Arrange
        var product = TestData.SampleProduct;
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _productService.GetByIdAsync(1);

        // Assert
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ProductDoesNotExist_ThrowsKeyNotFoundException()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ThrowsAsync(new KeyNotFoundException());

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ValidProduct_ReturnsCreatedProduct()
    {
        // Arrange
        var createRequest = TestData.CreateProductRequest;
        var product = new Product
        {
            Id = 1,
            Name = createRequest.Name,
            Price = createRequest.Price
        };
        _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(product);

        // Act
        var result = await _productService.AddAsync(createRequest);

        // Assert
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Price, result.Price);
    }

    [Fact]
    public async Task UpdateAsync_ValidProduct_UpdatesProductSuccessfully()
    {
        // Arrange
        var updateRequest = TestData.UpdateProductRequest;
        var product = new Product
        {
            Id = updateRequest.Id,
            Name = updateRequest.Name,
            Price = updateRequest.Price
        };

        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync(product);
        _productRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()));

        // Act
        var result = await _productService.UpdateAsync(updateRequest);

        // Assert
        Assert.Equal(updateRequest.Id, result.Id);
        Assert.Equal(updateRequest.Name, result.Name);
        Assert.Equal(updateRequest.Price, result.Price);
    }

    [Fact]
    public async Task DeleteAsync_ValidProduct_DeletesProductSuccessfully()
    {
        // Arrange
        var deleteRequest = TestData.DeleteProductRequest;

        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(deleteRequest.Id))
            .ReturnsAsync(new Product { Id = deleteRequest.Id });
        _productRepositoryMock.Setup(repo => repo.DeleteAsync(deleteRequest.Id));

        // Act
        await _productService.DeleteAsync(deleteRequest);

        // Assert
        _productRepositoryMock.Verify(repo => repo.DeleteAsync(deleteRequest.Id), Times.Once);
    }
}