using MotoApp.Components.CsvReader;
using MotoApp.Data;

namespace MotoApp.Components.CsvReaderApp;

internal class CsvReaderApp : ICsvReaderApp
{
    private readonly ICsvReader _csvReader;

    public CsvReaderApp(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }

    public void Run()
    {
        var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");
        string? choise;
        do
        {
            PrintMenuCsvReaderApp();
            choise = Console.ReadLine();

            if (choise == "1")
            {
                GroupByAndShowResult(cars);
            }
            else if (choise == "2")
            {
                JoinAndShowResult(cars, manufacturers);
            }
            else if (choise == "3")
            {
                GroupJoinAndShowResult(cars, manufacturers);
            }
            else
            {
                Console.WriteLine("Choose just  one from 1 to 3 or Q! No more options!");
                Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
            }
        } while (choise != "q" && choise != "Q");
    }

    private static void PrintMenuCsvReaderApp()
    {
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("Choose one option.");
        Console.WriteLine("\t1 - GroupBy");
        Console.WriteLine("\t2 - Join");
        Console.WriteLine("\t3 - GroupJoin");
        Console.WriteLine("\tQ - Exit");
        Console.Write("\t\tYour choise: ");
    }

    private static void GroupJoinAndShowResult(List<CsvReader.Models.Car> cars, List<CsvReader.Models.Manufacturer> manufacturers)
    {
        var groupJoin = manufacturers.GroupJoin(
                        cars,
                        m => m.Name,
                        c => c.Manufacturer,
                        (manufacturer, cars) => new
                        {
                            Manufacturer = manufacturer,
                            Cars = cars
                        })
                        .OrderBy(x => x.Manufacturer.Name);

        foreach (var group in groupJoin)
        {
            Console.WriteLine($"{group.Manufacturer.Name}");
            Console.WriteLine($"\tCars: {group.Cars.Count()}");
            Console.WriteLine($"\tMin: {group.Cars.Min(x => x.Combined)}");
            Console.WriteLine($"\tMax: {group.Cars.Max(x => x.Combined)}");
            Console.WriteLine($"\tAverage: {Math.Floor(group.Cars.Average(x => x.Combined))}");
        }
    }

    private static void JoinAndShowResult(List<CsvReader.Models.Car> cars, List<CsvReader.Models.Manufacturer> manufacturers)
    {
        var carsInCountry = cars.Join(
                        manufacturers,
                        c => new { c.Manufacturer, c.Year },
                        m => new { Manufacturer = m.Name, m.Year },
                        (car, manufacturer) => new
                        {
                            manufacturer.Country,
                            car.Name,
                            car.Combined
                        })
                        .OrderBy(x => x.Country)
                        .ThenBy(x => x.Combined);

        foreach (var car in carsInCountry)
        {
            Console.WriteLine($"Country: {car.Country}");
            Console.WriteLine($"\tName: {car.Name}");
            Console.WriteLine($"\tCombined: {car.Combined}");
        }
    }

    private static void GroupByAndShowResult(List<CsvReader.Models.Car> cars)
    {
        var groups = cars.GroupBy(x => x.Manufacturer)
                        .Select(x => new
                        {
                            Name = x.Key,
                            Max = x.Max(x => x.Combined),
                            Average = x.Average(x => x.Combined)
                        });

        foreach (var group in groups)
        {
            Console.WriteLine($"{group.Name}");
            Console.WriteLine($"\tMax: {group.Max}");
            Console.WriteLine($"\tAverage: {group.Average:0}");
        }
    }
}
