using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;

var sqlRepository = new SqlRepository<Employee>(new MotoAppDbContext());
sqlRepository.Add(new Employee { FirstName = "Greg" });
sqlRepository.Add(new Employee { FirstName = "Liz" });
sqlRepository.Add(new Employee { FirstName = "Chris" });
sqlRepository.Save();

var employee = sqlRepository.GetById(1);
Console.WriteLine(employee);