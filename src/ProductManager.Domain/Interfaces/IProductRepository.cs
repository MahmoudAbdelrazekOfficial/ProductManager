using ProductManager.Domain.Entities;

namespace ProductManager.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        // product-specific methods
    }
}
