using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;
using MotoApp.Repositories.Extensions;

var itemAdded = new ItemAddedDelegate<Employee>(EmployeeAdded);
var repository = new SqlRepository<Employee>(new MotoAppDbContext(), itemAdded);
AddEmployees(repository);
WriteAllToConsole(repository);

static void EmployeeAdded(Employee item)
{
    Console.WriteLine($"{item.FirstName} added.");
}

static void AddEmployees(IRepository<Employee> repository)
{
    var employees = new[]
    {
        new Employee { FirstName = "Greg"},
        new Employee { FirstName = "Liz"},
        new Employee { FirstName = "Chris"}
    };

    repository.AddBatch(employees);
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    foreach (var item in repository.GetAll())
    {
        Console.WriteLine(item);
    }
}