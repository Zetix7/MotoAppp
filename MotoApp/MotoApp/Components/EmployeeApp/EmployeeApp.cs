using MotoApp.Components.DataProviders;
using MotoApp.Data;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

namespace MotoApp.Components.EmployeeApp;

public class EmployeeApp : IEmployeeApp
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IEmployeeProvider _employeeProvider;

    public EmployeeApp(IRepository<Employee> employeeRepository, IEmployeeProvider employeeProvider)
    {
        _employeeRepository = employeeRepository;
        _employeeProvider = employeeProvider;
    }

    public void Run()
    {
        _employeeRepository.ItemAdded += EmployeeRepositoryOnItemAdded;
        _employeeRepository.ItemRemoved += EmployeeRepositoryOnItemRemoved;

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("Welcome in part of Employees Mangement App");

        string? choise;
        do
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("You can READ or ADD employees from/to file.");
            Console.WriteLine("\t1 - Read employees from file");
            Console.WriteLine("\t2 - Add employees to file");
            Console.WriteLine("\t3 - Get specific data");
            Console.WriteLine("\tQ - Exit");
            Console.Write("Your choise: ");
            choise = Console.ReadLine();

            if (choise == "1")
            {
                ReadEmployeeFromJsonFile();
            }
            else if (choise == "2")
            {
                AddEmployeeToJsonFile();
            }
            else if (choise == "3")
            {
                GetSpecificEmployeeData();
            }
            else
            {
                Console.WriteLine("\tTry again!");
            }
        } while (choise != "q" && choise != "Q");
    }

    private void AddEmployeeToJsonFile()
    {
        try
        {
            Console.Write("Insert employee name: ");
            var employeeName = Console.ReadLine();
            var data = new DataInFile<Employee>(_employeeRepository);
            data.ItemAdded += EmployeeFileOnItemAdded;
            data.Add(new Employee { FirstName = employeeName });
        }
        catch (FileNotFoundException fe)
        {
            Console.WriteLine($"Exception catched: {fe.Message}");
        }
        catch (ArgumentOutOfRangeException ae)
        {
            Console.WriteLine($"Exception catched: {ae.Message}");
        }
        catch (NullReferenceException ne)
        {
            Console.WriteLine($"Exception catched: {ne.Message}");
        }
    }

    private void ReadEmployeeFromJsonFile()
    {
        try
        {
            var data = new DataInFile<Employee>(_employeeRepository);
            data.ItemRead += EmployeesFileOnItemRead;
            data.Read();
            WriteAllToConsole(_employeeRepository);
        }
        catch (FileNotFoundException fe)
        {
            Console.WriteLine($"Exception catched: {fe.Message}");
        }
        catch (FileLoadException fe)
        {
            Console.WriteLine($"Exception catched: {fe.Message}");
        }
    }

    private void GetSpecificEmployeeData()
    {
        string? choise;
        do
        {
            ShowMenuEmployeeApp();
            choise = Console.ReadLine();

            switch (choise)
            {
                case "1":
                    OrderEmployeesByName();
                    break;
                case "2":
                    GetUniqueEmployeesName();
                    break;
                case "3":
                    GetChunkEmployees();
                    break;
                case "4":
                    SkipEmployees();
                    break;
                case "5":
                    GetFirstEmployeeStartsWith();
                    break;
                case "Q" or "q":
                    break;
                default:
                    Console.WriteLine("\tTry again");
                    break;
            }
        } while (choise != "Q" && choise != "q");
    }

    private static void ShowMenuEmployeeApp()
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("Get specific useless data");
        Console.WriteLine("\t1 - Order Employees By Name");
        Console.WriteLine("\t2 - Get Unique Employees Name");
        Console.WriteLine("\t3 - Get Chunk Employees");
        Console.WriteLine("\t4 - Skip Employees");
        Console.WriteLine("\t5 - Get Single Employee Starts With");
        Console.WriteLine("\tQ - Exit");
        Console.Write("Your choise: ");
    }

    private void OrderEmployeesByName()
    {
        foreach (var employee in _employeeProvider.OrderEmployeesByName())
        {
            Console.WriteLine("\t" + employee);
        }
    }

    private void GetUniqueEmployeesName()
    {
        foreach (var employee in _employeeProvider.GetUniqueEmployeesName())
        {
            Console.WriteLine("\t" + employee);
        }
    }

    private void GetChunkEmployees()
    {
        do
        {
            Console.Write("Insert size of chunk (must be uint!): ");
            var choise = Console.ReadLine();

            if (int.TryParse(choise, out int size))
            {
                foreach (var chunk in _employeeProvider.GetChunkEmployees(size))
                {
                    Console.WriteLine("---chunk---");
                    foreach (var employee in chunk)
                    {
                        Console.WriteLine("\t" + employee);
                    }
                }
                break;
            }
            else
            {
                Console.WriteLine("Try again! Insert correct uint!");
            }
        } while (true);
    }

    private void SkipEmployees()
    {
        do
        {
            Console.Write("Insert how many skip employees (must be uint!): ");
            var choise = Console.ReadLine();

            if (int.TryParse(choise, out int howMany))
            {
                foreach (var employee in _employeeProvider.SkipEmployees(howMany))
                {
                    Console.WriteLine("\t" + employee);
                }
                break;
            }
            else
            {
                Console.WriteLine("Try again! Insert correct uint!");
            }
        } while (true);
    }

    private void GetFirstEmployeeStartsWith()
    {
        Console.Write("Insert employee name starts with letter/s...: ");
        var choise = Console.ReadLine();
        Console.WriteLine("\t" + _employeeProvider.GetFirstEmployeeStartsWith(choise!));
    }

    private static void FillAuditFile(string action, string item)
    {
        using (var writer = File.AppendText("audit.txt"))
        {
            writer.WriteLine($"[{DateTime.Now}]-{action,-29}-[{item}]");
        }
    }

    private static void EmployeeFileOnItemAdded(object? sender, Employee e)
    {
        Console.WriteLine($"\tAdded to file. Employee: {e.FirstName} execute from {sender?.GetType().Name}");
        FillAuditFile("EmployeeAddedToFile", e.FirstName!);
    }

    private static void EmployeesFileOnItemRead(object? sender, Employee e)
    {
        Console.WriteLine($"\tRead from file. Employee: {e.FirstName} execute from {sender?.GetType().Name}");
        FillAuditFile("EmployeeReadFromFile", e.FirstName!);
    }

    private static void EmployeeRepositoryOnItemAdded(object? sender, Employee e)
    {
        Console.WriteLine($"\tAdded to Repository. Employee: {e.FirstName} execute form {sender?.GetType().Name}");
        FillAuditFile("EmployeeAddedToRepository", e.FirstName!);
    }

    private static void EmployeeRepositoryOnItemRemoved(object? sender, Employee e)
    {
        Console.WriteLine($"\tRemoved from Repository. Employee: {e.FirstName} execute form {sender?.GetType().Name}");
        FillAuditFile("EmployeeRemovedFromRepository", e.FirstName!);
    }

    private static void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
        foreach (var item in repository.GetAll())
        {
            Console.WriteLine(item);
        }
    }
}
