using MotoApp.Entities;

namespace MotoApp.Repositories;

public class GenericRepositoryWithRemove<T, TKey> : GenericRepository<T, TKey> 
    where T : class, IEntity, new()
    where TKey : struct
{
    public void Remove(T item)
    {
        _items.Remove(item);
    }
}
