using MotoApp.Entities;
using MotoApp.Repositories;

var employeeRepository = new GenericRepository<Employee>();
employeeRepository.Add(new Employee { FirstName = "Greg" });
employeeRepository.Add(new Employee { FirstName = "Liz" });
employeeRepository.Add(new Employee { FirstName = "Chris" });
employeeRepository.Save();