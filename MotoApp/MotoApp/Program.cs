using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;

var repository = new SqlRepository<Employee>(new MotoAppDbContext());
AddEmployees(repository);
WriteAllToConsole(repository);

static void AddEmployees(IRepository<Employee> repository)
{
    var employees = new[]
    {
        new Employee { FirstName = "Greg"},
        new Employee { FirstName = "Liz"},
        new Employee { FirstName = "Chris"}
    };

    AddBatch(repository, employees);
}

static void AddBatch<T>(IRepository<T> repository, IEnumerable<T> items) where T : class,IEntity
{
    foreach (var item in items)
    {
        repository.Add(item);
    }
    repository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    foreach (var item in repository.GetAll())
    {
        Console.WriteLine(item);
    }
}