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
                var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");
                foreach (var car in cars.Where(x=>x.Manufacturer == "Audi"))
                {
                    Console.WriteLine(car.Year);
                    Console.WriteLine($"\t{car.Manufacturer} {car.Name}");
                    Console.WriteLine("\tDisplacement: " + car.Displacement);
                    Console.WriteLine("\tCylinders: " + car.Cylinders);
                    Console.WriteLine("\tCity: " + car.City);
                    Console.WriteLine("\tHighway: " + car.Highway);
                    Console.WriteLine("\tCombined: " + car.Combined);
                }
            }
            else
            {
                Console.WriteLine("Choose 1 or 2 or Q! No more options!");
                Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
            }
        } while (true);
    }
}
