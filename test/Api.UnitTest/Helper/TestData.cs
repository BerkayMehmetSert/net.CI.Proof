using Api.Application.Request;
using Api.Application.Response;
using Api.Domain;

namespace Api.UnitTest.Helper;

public static class TestData
{
    public static Product SampleProduct => new()
    {
        Id = 1,
        Name = "Sample Product",
        Price = 100
    };

    public static Product AnotherSampleProduct => new()
    {
        Id = 2,
        Name = "Another Sample Product",
        Price = 200
    };

    public static CreateProductRequest CreateProductRequest => new()
    {
        Name = "New Product",
        Price = 150
    };

    public static CreateProductResponse CreateProductResponse => new()
    {
        Id = 1,
        Name = "New Product",
        Price = 150
    };

    public static UpdateProductRequest UpdateProductRequest => new()
    {
        Id = 1,
        Name = "Updated Product",
        Price = 250
    };

    public static UpdateProductResponse UpdateProductResponse => new()
    {
        Id = 1,
        Name = "Updated Product",
        Price = 250
    };

    public static DeleteProductRequest DeleteProductRequest => new(1);
}