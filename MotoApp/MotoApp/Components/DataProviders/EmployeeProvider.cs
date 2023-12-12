using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

namespace MotoApp.Components.DataProviders;

public class EmployeeProvider : IEmployeeProvider
{
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeeProvider(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public List<Employee[]> GetChunkEmployees(int size)
    {
        var employees = _employeeRepository.GetAll();
        return employees.Chunk(size).ToList();
    }

    public Employee? GetFirstEmployeeStartsWith(string prefix)
    {
        var employees = _employeeRepository.GetAll();
        return employees.FirstOrDefault(x => x.FirstName!.StartsWith(prefix), new Employee { Id = -1, FirstName = "NOT FOUND" });
    }

    public List<string> GetUniqueEmployeesName()
    {
        var employees = _employeeRepository.GetAll();
        return employees.Select(x => x.FirstName!).Distinct().ToList();
    }

    public List<Employee> OrderEmployeesByName()
    {
        var employees = _employeeRepository.GetAll();
        return employees.OrderBy(x => x.FirstName).ToList();
    }

    public List<Employee> SkipEmployees(int howMany)
    {
        var employees = _employeeRepository.GetAll();
        return employees.Skip(howMany).ToList();
    }
}
