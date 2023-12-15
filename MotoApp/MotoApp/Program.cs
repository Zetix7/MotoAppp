using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MotoApp;
using MotoApp.Components.CsvReader;
using MotoApp.Components.CsvReaderApp;
using MotoApp.Components.DataProviders;
using MotoApp.Components.EmployeeApp;
using MotoApp.Components.MotoAppStorageAccess;
using MotoApp.Components.MotoAppStorageAccessApp;
using MotoApp.Components.XmlReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEmployeeApp, EmployeeApp>();
services.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
//services.AddSingleton<IRepository<Car>, ListRepository<Car>>();
//services.AddSingleton<IRepository<Manufacturer>, ListRepository<Manufacturer>>();
services.AddSingleton<IEmployeeProvider, EmployeeProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<ICsvReaderApp, CsvReaderApp>();
services.AddDbContext<MotoAppDbContext>(options => options
    .UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MotoAppStorage;Integrated Security=True;Encrypt=False"));
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IMotoAppStorageAccess, MotoAppStorageAccess>();
services.AddSingleton<IMotoAppStorageAccessApp, MotoAppStorageAccessApp>();
//services.AddSingleton<IRepository<Employee>, SqlRepository<Employee>>();
services.AddSingleton<IRepository<Car>, SqlRepository<Car>>();
services.AddSingleton<IRepository<Manufacturer>, SqlRepository<Manufacturer>>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
