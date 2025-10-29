using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Entities;

namespace ProductManager.Infrastructure.Data;

public class MyAppDbContext : DbContext
{
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Name).IsRequired().HasMaxLength(200);
            b.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });
    }
}
