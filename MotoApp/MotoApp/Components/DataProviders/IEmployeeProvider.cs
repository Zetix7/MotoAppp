using MotoApp.Data.Entities;

namespace MotoApp.Components.DataProviders;

public interface IEmployeeProvider
{
    List<Employee> OrderEmployeesByName();
    List<string> GetUniqueEmployeesName();
    List<Employee[]> GetChunkEmployees(int size);
    List<Employee> SkipEmployees(int howMany);
    Employee? GetFirstEmployeeStartsWith(string prefix);
}
