using System;

namespace ProductManager.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Product() { }

    public Product(string name, decimal price, string? description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, decimal price, string? description)
    {
        Name = name;
        Price = price;
        Description = description;
    }
}
