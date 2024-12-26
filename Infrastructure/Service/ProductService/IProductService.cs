using Domain.Entities;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.ProductService;

public interface IProductService
{
    Task<Response<List<Product>>> GetAll();
    Task<Response<Product>> GetProduct(int id);
    Task<Response<string>> AddProduct(Product request);
    Task<Response<string>> UpdateProduct(Product request);
    Task<Response<string>> DeleteProduct(int id);
}