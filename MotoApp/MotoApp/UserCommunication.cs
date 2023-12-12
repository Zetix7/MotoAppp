using MotoApp.Components.CarApp;
using MotoApp.Components.CsvReaderApp;
using MotoApp.Components.EmployeeApp;

namespace MotoApp;

public class UserCommunication : IUserCommunication
{
    private readonly IEmployeeApp _employeeApp;
    private readonly ICarApp _carApp;
    private readonly ICsvReaderApp _csvReaderApp;

    public UserCommunication(IEmployeeApp employeeApp, ICarApp carApp, ICsvReaderApp csvReaderApp)
    {
        _employeeApp = employeeApp;
        _carApp = carApp;
        _csvReaderApp = csvReaderApp;
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
                _csvReaderApp.Run();
            }
            else
            {
                Console.WriteLine("Choose 1 or 2 or 3 or Q! No more options!");
                Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
            }
        } while (true);
    }
}
