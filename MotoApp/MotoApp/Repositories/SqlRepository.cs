using Microsoft.EntityFrameworkCore;
using MotoApp.Entities;

namespace MotoApp.Repositories;

public delegate void ItemAddedDelegate(object item);

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;
    private readonly ItemAddedDelegate _itemAddedDelegate;

    public SqlRepository(DbContext dbContext, ItemAddedDelegate itemAddedDelegate = null)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        _itemAddedDelegate = itemAddedDelegate;

    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.OrderBy(x => x.Id).ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        _itemAddedDelegate?.Invoke(item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
