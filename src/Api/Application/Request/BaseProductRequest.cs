namespace Api.Application.Request;

public class BaseProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}