using MotoApp.Entities;
using MotoApp.Repositories;
using MotoApp.Repositories.Extensions;

namespace MotoApp;

public class App : IApp
{
    private readonly IRepository<Employee> _employeeRepository;

    public App(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void Run()
    {
        Console.WriteLine("I am in Run() method");

        var employees = new[]
        {
            new Employee { FirstName="Greg"},
            new Employee { FirstName="Liz"},
            new Employee { FirstName="Scarlett"}
        };

        _employeeRepository.AddBatch(employees);

        foreach (var employee in _employeeRepository.GetAll())
        {
            Console.WriteLine(employee);
        }
    }
}
