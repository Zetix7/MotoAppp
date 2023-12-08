using MotoApp.Entities;

namespace MotoApp.Repositories;

public class GenericRepositoryWithRemove<T, TKey> : GenericRepository<T, TKey> where T : IEntity
{
    public void Remove(T item)
    {
        _items.Remove(item);
    }
}
