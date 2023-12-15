using MotoApp.Components.CsvReaderApp;
using MotoApp.Components.EmployeeApp;
using MotoApp.Components.MotoAppStorageAccessApp;

namespace MotoApp;

public class UserCommunication : IUserCommunication
{
    private readonly IEmployeeApp _employeeApp;
    private readonly ICsvReaderApp _csvReaderApp;
    private readonly IMotoAppStorageAccessApp _motoAppStorageAccessApp;

    public UserCommunication(IEmployeeApp employeeApp, ICsvReaderApp csvReaderApp, IMotoAppStorageAccessApp motoAppStorageAccessApp)
    {
        _employeeApp = employeeApp;
        _csvReaderApp = csvReaderApp;
        _motoAppStorageAccessApp = motoAppStorageAccessApp;
    }

    public void Run()
    {
        Console.WriteLine("Welcome in complicated App!\n");

        do
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Choose one option.");
            Console.WriteLine("\t1 - Employees Management App");
            Console.WriteLine("\t2 - Csv files App");
            Console.WriteLine("\t3 - Moto Management App");
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
                _csvReaderApp.Run();
            }
            else if (choise == "3")
            {
                _motoAppStorageAccessApp.Run();
            }
            else
            {
                Console.WriteLine("Choose 1 or 2 or Q! No more options!");
                Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
            }
        } while (true);
    }
}
