using MotoApp.Components.CsvReader;
using MotoApp.Components.CsvReader.Models;

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

        foreach(var car in carsInCountry)
        {
            Console.WriteLine($"Country: {car.Country}");
            Console.WriteLine($"\tName: {car.Name}");
            Console.WriteLine($"\tCombined: {car.Combined}");
        }

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
            Console.WriteLine($"\tMin: {group.Cars.Min(x=>x.Combined)}");
            Console.WriteLine($"\tMax: {group.Cars.Max(x => x.Combined)}");
            Console.WriteLine($"\tAverage: {Math.Floor(group.Cars.Average(x => x.Combined))}");
        }
    }
}
