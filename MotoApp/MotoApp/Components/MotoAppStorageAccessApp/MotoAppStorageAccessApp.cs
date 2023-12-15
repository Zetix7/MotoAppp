using MotoApp.Components.MotoAppStorageAccess;

namespace MotoApp.Components.MotoAppStorageAccessApp;

public class MotoAppStorageAccessApp : IMotoAppStorageAccessApp
{
    private readonly IMotoAppStorageAccess _motoAppStorageAccess;

    public MotoAppStorageAccessApp(IMotoAppStorageAccess motoAppStorageAccess)
    {
        _motoAppStorageAccess = motoAppStorageAccess;
    }

    public void Run()
    {
            Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine("Welcome in part of Moto Mangement App");
        var choise = "";
        do
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("You can ADD, READ, UPDATE or REMOVE cars or manufacturers from/to repository.");
            Console.WriteLine("\t1 - Cars");
            Console.WriteLine("\t2 - Manufacturers");
            Console.WriteLine("\tQ - Exit");
            Console.Write("Your choise: ");
            choise = Console.ReadLine();

            if (choise == "q" || choise == "Q")
            {
                break;
            }
            else if (choise == "1")
            {
                Console.WriteLine("Choose one option:");
                Console.WriteLine("\t1. Insert cars to repository.");
                Console.WriteLine("\t2. Read cars from repository.");
                Console.WriteLine("\t3. Update car name in repository.");
                Console.WriteLine("\t4. Remove car by name from repository.");
                Console.Write("\t\tYour choise: ");

                choise = Console.ReadLine();
                if (choise == "1")
                {
                    _motoAppStorageAccess.InsertCarsToDatabase();
                }
                else if (choise == "2")
                {
                    _motoAppStorageAccess.ReadCarsFromDatabase();
                }
                else if (choise == "3")
                {
                    Console.Write($"\tInsert car name to update: ");
                    var oldName = Console.ReadLine();
                    Console.Write($"\tInsert new car name: ");
                    var newName = Console.ReadLine();
                    _motoAppStorageAccess.UpdateCarNameInDatabase(oldName!, newName!);
                }
                else if (choise == "4")
                {
                    Console.Write($"\tInsert car name to remove: ");
                    var name = Console.ReadLine();
                    _motoAppStorageAccess.RemoveCarByNameFromDatabase(name!);
                }
            }
            else if (choise == "2")
            {
                Console.WriteLine("Choose one option:");
                Console.WriteLine("\t1. Insert manufacturers to repository.");
                Console.WriteLine("\t2. Read manufacturers from repository.");
                Console.WriteLine("\t3. Update manufacturer country by name in repository.");
                Console.WriteLine("\t4. Remove manufacturer by name from repository.");
                Console.Write("\t\tYour choise: ");
                choise = Console.ReadLine();

                if (choise == "1")
                {
                    _motoAppStorageAccess.InsertManufacturersToDatabase();
                }
                else if (choise == "2")
                {
                    _motoAppStorageAccess.ReadManufacturersFromDatabase();
                }
                else if (choise == "3")
                {
                    Console.Write($"\tInsert manufacturer name to update: ");
                    var name = Console.ReadLine();
                    Console.Write($"\tInsert new manufacturer country: ");
                    var newCountry = Console.ReadLine();
                    _motoAppStorageAccess.UpdateManufacturerCountryInDatabase(name!, newCountry!);
                }
                else if (choise == "4")
                {
                    Console.Write($"\tInsert manufacturer name to remove: ");
                    var name = Console.ReadLine();
                    _motoAppStorageAccess.RemoveManufacturerByNameFromDatabase(name!);
                }
            }
            else
            {
                Console.WriteLine("\tTry again!");
            }
        } while (choise != "q" || choise != "Q");
    }
}
