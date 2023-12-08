using MotoApp.Entities;

namespace MotoApp.Repositories;

internal interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
}
