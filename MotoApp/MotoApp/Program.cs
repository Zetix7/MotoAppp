using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;

var repository = new SqlRepository<Employee>(new MotoAppDbContext());
AddEmployees(repository);
AddManagers(repository);
WriteAllToConsole(repository);

static void AddEmployees(IRepository<Employee> repository)
{
    repository.Add(new Employee { FirstName = "Greg" });
    repository.Add(new Employee { FirstName = "Liz" });
    repository.Add(new Employee { FirstName = "Chris" });
    repository.Save();
}

static void AddManagers(IWriteRepository<Manager> repository)
{
    repository.Add(new Manager { FirstName = "Natalie" });
    repository.Add(new Manager { FirstName = "Scarlett" });
    repository.Add(new Manager { FirstName = "Robert" });
    repository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    foreach (var item in repository.GetAll())
    {
        Console.WriteLine(item);
    }
}