using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Interfaces;
using ProductManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MyAppDbContext _db;
    protected readonly DbSet<T> _set;

    public Repository(MyAppDbContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _set.AddAsync(entity, ct);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken ct = default) =>
        await _set.AsNoTracking().ToListAsync(ct);

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _set.FindAsync(new object[] { id }, ct) as T;
    }

    public void Remove(T entity) => _set.Remove(entity);

    public void Update(T entity) => _set.Update(entity);
}
