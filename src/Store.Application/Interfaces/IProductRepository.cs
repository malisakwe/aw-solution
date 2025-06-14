using Store.Domain.Entities;

namespace Store.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
}