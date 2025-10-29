using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using ProductManager.Infrastructure.Data;

namespace ProductManager.Infrastructure.Repositories;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MyAppDbContext db) : base(db) { }
}
