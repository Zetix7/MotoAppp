using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;

var employeeRepository = new GenericRepository<Employee>();
employeeRepository.Add(new Employee { FirstName = "Greg" });
employeeRepository.Add(new Employee { FirstName = "Liz" });
employeeRepository.Add(new Employee { FirstName = "Chris" });
employeeRepository.Save();

var sqlRepository = new SqlRepository(new MotoAppDbContext());
sqlRepository.Add(new Employee { FirstName = "Greg" });
sqlRepository.Add(new Employee { FirstName = "Liz" });
sqlRepository.Add(new Employee { FirstName = "Chris" });
sqlRepository.Save();

var employee = sqlRepository.GetById(1);
Console.WriteLine(employee);