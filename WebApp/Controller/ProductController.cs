using Domain.Entities;
using Infrastructure.ApiResponse;
using Infrastructure.Service.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebApp.Controller;




[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service)
{
    [HttpGet]
    public async Task<Response<List<Product>>> GetAll() => await service.GetAll();
    
    [HttpGet("{id}")]
    public async Task<Response<Product>> GetById(int id) => await service.GetProduct(id);

    [HttpPost]
    public async Task<Response<string>> Add(Product product) => await service.AddProduct(product);

    [HttpPut]
    public async Task<Response<string>> Update(Product product) => await service.UpdateProduct(product);

    [HttpDelete]
    public async Task<Response<string>> Delete(int id) => await service.DeleteProduct(id);
}