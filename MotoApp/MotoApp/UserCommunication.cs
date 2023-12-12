using MotoApp.Components;
using MotoApp.Components.CsvReader;

namespace MotoApp;

public class UserCommunication : IUserCommunication
{
    private IEmployeeApp _employeeApp;
    private ICarApp _carApp;
    private ICsvReader _csvReader;

    public UserCommunication(IEmployeeApp employeeApp, ICarApp carApp, ICsvReader csvReader)
    {
        _employeeApp = employeeApp;
        _carApp = carApp;
        _csvReader = csvReader;
    }

    public void Run()
    {
        Console.WriteLine("Welcome in complicated App!\n");

        do
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Choose one option.");
            Console.WriteLine("\t1 - Employees Management App");
            Console.WriteLine("\t2 - Cars Management App");
            Console.WriteLine("\t3 - Csv files App");
            Console.WriteLine("\tQ - Exit");
            Console.Write("\t\tYour choise: ");
            var choise = Console.ReadLine();

            if (choise == "q" || choise == "Q")
            {
                break;
            }
            else if (choise == "1")
            {
                _employeeApp.Run();
            }
            else if (choise == "2")
            {
                _carApp.ShowDataOfCarsExample();
            }
            else if (choise == "3")
            {
                Console.WriteLine("fuel.csv");
                var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");
                var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");
                
                var groups = cars.GroupBy(x => x.Manufacturer)
                    .Select(x=> new
                    {
                        Name = x.Key,
                        Max = x.Max(x=>x.Combined),
                        Average = x.Average(x=>x.Combined)
                    });

                foreach(var group in groups)
                {
                    Console.WriteLine($"{group.Name}");
                    Console.WriteLine($"\tMax: {group.Max}");
                    Console.WriteLine($"\tAverage: {group.Average:0}");
                }
            }
            else
            {
                Console.WriteLine("Choose 1 or 2 or 3 or Q! No more options!");
                Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
            }
        } while (true);
    }
}
