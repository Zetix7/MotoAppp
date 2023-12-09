using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;
using MotoApp.Repositories.Extensions;

var repository = new SqlRepository<Employee>(new MotoAppDbContext(), EmployeeAdded);
AddEmployees(repository);
WriteAllToConsole(repository);

static void EmployeeAdded(object item)
{
    var employee = (Employee)item;
    Console.WriteLine($"{employee.FirstName} added.");
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