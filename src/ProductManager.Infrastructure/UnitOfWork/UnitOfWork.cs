using ProductManager.Domain.Interfaces;
using ProductManager.Infrastructure.Data;


namespace ProductManager.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyAppDbContext _db;
    private readonly Dictionary<Type, object> _repos = new();

    public UnitOfWork(MyAppDbContext db) => _db = db;

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        if (_repos.ContainsKey(type)) return (IRepository<T>)_repos[type];

        var repoType = typeof(ProductManager.Infrastructure.Repositories.Repository<>).MakeGenericType(type);
        var repoInstance = Activator.CreateInstance(repoType, _db);
        if (repoInstance == null) throw new InvalidOperationException("Could not create repository instance");

        _repos[type] = repoInstance;
        return (IRepository<T>)repoInstance;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _db.SaveChangesAsync(ct);

    public void Dispose() => _db.Dispose();
}
