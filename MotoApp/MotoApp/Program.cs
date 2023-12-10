﻿using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;
using MotoApp.Repositories.Extensions;

var repository = new SqlRepository<Employee>(new MotoAppDbContext());
repository.ItemAdded += EmployeeRepositoryOnItemAdded;
repository.ItemRemoved += EmployeeRepositoryOnItemRemoved;

Console.WriteLine("Welcome in Employees Mangement App\n");

do
{
    Console.WriteLine("-------------------------------------------------------------------");
    Console.WriteLine("You can READ or ADD employees from/to file.");
    Console.WriteLine("\t1 - Read employees from file");
    Console.WriteLine("\t2 - Add employees to file");
    Console.WriteLine("\tQ - Exit");
    Console.Write("Your choise: ");
    var choise = Console.ReadLine();

    if (choise == "q" || choise == "Q")
    {
        break;
    }
    else if (choise == "1")
    {
        try
        {
            var data = new DataInFile<Employee>(repository);
            data.ItemRead += EmployeesFileOnItemRead;
            data.Read();
            WriteAllToConsole(repository);
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
    else if (choise == "2")
    {
        try
        {
            Console.Write("Insert employee name: ");
            var employeeName = Console.ReadLine();
            var data = new DataInFile<Employee>(repository);
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
        catch(NullReferenceException ne)
        {
            Console.WriteLine($"Exception catched: {ne.Message}");
        }
    }
    else
    {
        Console.WriteLine("\tTry again!");
    }
} while (true);

static void EmployeeFileOnItemAdded(object? sender, Employee e)
{
    Console.WriteLine($"\tAdded to file. Employee: {e.FirstName} execute from {sender?.GetType().Name}");
}

static void EmployeesFileOnItemRead(object? sender, Employee e)
{
    Console.WriteLine($"\tRead from file. Employee: {e.FirstName} execute from {sender?.GetType().Name}");
}

static void EmployeeRepositoryOnItemAdded(object? sender, Employee e)
{
    Console.WriteLine($"\tAdded to Repository. Employee: {e.FirstName} execute form {sender?.GetType().Name}");
}

static void EmployeeRepositoryOnItemRemoved(object? sender, Employee e)
{
    Console.WriteLine($"\tRemoved from Repository. Employee: {e.FirstName} execute form {sender?.GetType().Name}");
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