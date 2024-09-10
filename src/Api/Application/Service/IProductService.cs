using Api.Application.Request;
using Api.Application.Response;

namespace Api.Application.Service;

public interface IProductService
{
    Task<IEnumerable<GetProductResponse>> GetAllAsync();
    Task<GetProductByIdResponse> GetByIdAsync(int id);
    Task<CreateProductResponse> AddAsync(CreateProductRequest request);
    Task<UpdateProductResponse> UpdateAsync(UpdateProductRequest request);
    Task DeleteAsync(DeleteProductRequest request);
}