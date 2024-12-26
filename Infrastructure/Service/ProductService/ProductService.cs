using System.Net;
using Domain.Entities;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.ProductService;

public class ProductService(DataContext context) : IProductService
{
    public async Task<Response<List<Product>>> GetAll()
    {
        var products = await context.Products.ToListAsync();
        return new Response<List<Product>>(products);
    }

    public async Task<Response<Product>> GetProduct(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(g => g.Id == id);
        return product == null
            ? new Response<Product>(HttpStatusCode.NotFound, "Product not found")
            : new Response<Product>(product);
    }

    public async Task<Response<string>> AddProduct(Product request)
    {
        await context.Products.AddAsync(request);
        var result = await context.SaveChangesAsync();
        
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Product not created")
            : new Response<string>("Product created successfully");
    }

    public async Task<Response<string>> UpdateProduct(Product request)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(g => g.Id == request.Id);
        if (existingProduct == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found");
        }

        existingProduct.Name = request.Name;
        existingProduct.Price = request.Price;
        existingProduct.CategoryName = request.CategoryName;

        var result = await context.SaveChangesAsync();
        
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Product not updated")
            : new Response<string>("Product updated successfully");
    }

    public async Task<Response<string>> DeleteProduct(int id)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(g => g.Id == id);
        
        if (existingProduct == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found");
        }

        context.Remove(existingProduct);
        var result = await context.SaveChangesAsync();
        
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Product not deleted")
            : new Response<string>("Product deleted successfully");

    }
}