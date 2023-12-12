using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

namespace MotoApp.Data.Repositories.Extensions;

public static class RepositoryExtension
{
    public static void AddBatch<T>(this IRepository<T> repository, IEnumerable<T> items) where T : class, IEntity
    {
        foreach (var item in items)
        {
            repository.Add(item);
        }
        repository.Save();
    }
}
