using Microsoft.Extensions.DependencyInjection;
using MotoApp;
using MotoApp.Components.CarApp;
using MotoApp.Components.CsvReader;
using MotoApp.Components.CsvReaderApp;
using MotoApp.Components.DataProviders;
using MotoApp.Components.EmployeeApp;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEmployeeApp, EmployeeApp>();
services.AddSingleton<ICarApp, CarApp>();
services.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
services.AddSingleton<IRepository<Car>, ListRepository<Car>>();
services.AddSingleton<ICarsProvider, CarsProvider>();
services.AddSingleton<IEmployeeProvider, EmployeeProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<ICsvReaderApp, CsvReaderApp>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
