using MotoApp.Entities;

namespace MotoApp.Repositories.Extensions;

public static class RepositoryExtension
{
    public static void AddBatch<T>(this IRepository<T> repository, IEnumerable<T> items) where T : class, IEntity
    {
        foreach(var item in items)
        {
            repository.Add(item);
        }
        repository.Save();
    }
}
