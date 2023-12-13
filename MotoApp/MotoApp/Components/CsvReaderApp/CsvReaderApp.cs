using MotoApp.Components.CsvReader;
using MotoApp.Data;
using System.Xml.Linq;

namespace MotoApp.Components.CsvReaderApp;

internal class CsvReaderApp : ICsvReaderApp
{
    private readonly ICsvReader _csvReader;
    private readonly MotoAppDbContext _motoAppDbContext;

    public CsvReaderApp(ICsvReader csvReader, MotoAppDbContext motoAppDbContext)
    {
        _csvReader = csvReader;
        _motoAppDbContext = motoAppDbContext;
        _motoAppDbContext.Database.EnsureCreated();
    }

    public void Run()
    {
        var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");



        //do
        //{
        //    Console.WriteLine("-------------------------------------------------------------");
        //    Console.WriteLine("Choose one option.");
        //    Console.WriteLine("\t1 - GroupBy");
        //    Console.WriteLine("\t2 - Join");
        //    Console.WriteLine("\t3 - GroupJoin");
        //    Console.WriteLine("\t4 - Create fuel.xml file");
        //    Console.WriteLine("\t5 - Read fuel.xml file");
        //    Console.WriteLine("\t6 - Print datas and create manufacturersFuel.xml file");
        //    Console.WriteLine("\tQ - Exit");
        //    Console.Write("\t\tYour choise: ");
        //    var choise = Console.ReadLine();

        //    if (choise == "q" || choise == "Q")
        //    {
        //        break;
        //    }
        //    else if (choise == "1")
        //    {
        //        var groups = cars.GroupBy(x => x.Manufacturer)
        //        .Select(x => new
        //        {
        //            Name = x.Key,
        //            Max = x.Max(x => x.Combined),
        //            Average = x.Average(x => x.Combined)
        //        });

        //        foreach (var group in groups)
        //        {
        //            Console.WriteLine($"{group.Name}");
        //            Console.WriteLine($"\tMax: {group.Max}");
        //            Console.WriteLine($"\tAverage: {group.Average:0}");
        //        }
        //    }
        //    else if (choise == "2")
        //    {
        //        var carsInCountry = cars.Join(
        //        manufacturers,
        //        c => new { c.Manufacturer, c.Year },
        //        m => new { Manufacturer = m.Name, m.Year },
        //        (car, manufacturer) => new
        //        {
        //            manufacturer.Country,
        //            car.Name,
        //            car.Combined
        //        })
        //        .OrderBy(x => x.Country)
        //        .ThenBy(x => x.Combined);

        //        foreach (var car in carsInCountry)
        //        {
        //            Console.WriteLine($"Country: {car.Country}");
        //            Console.WriteLine($"\tName: {car.Name}");
        //            Console.WriteLine($"\tCombined: {car.Combined}");
        //        }
        //    }
        //    else if (choise == "3")
        //    {
        //        var groupJoin = manufacturers.GroupJoin(
        //        cars,
        //        m => m.Name,
        //        c => c.Manufacturer,
        //        (manufacturer, cars) => new
        //        {
        //            Manufacturer = manufacturer,
        //            Cars = cars
        //        })
        //        .OrderBy(x => x.Manufacturer.Name);

        //        foreach (var group in groupJoin)
        //        {
        //            Console.WriteLine($"{group.Manufacturer.Name}");
        //            Console.WriteLine($"\tCars: {group.Cars.Count()}");
        //            Console.WriteLine($"\tMin: {group.Cars.Min(x => x.Combined)}");
        //            Console.WriteLine($"\tMax: {group.Cars.Max(x => x.Combined)}");
        //            Console.WriteLine($"\tAverage: {Math.Floor(group.Cars.Average(x => x.Combined))}");
        //        }
        //    }
        //    else if (choise == "4")
        //    {
        //        var document = new XDocument();

        //        document.Add(
        //            new XElement("Cars", cars
        //            .Select(x =>
        //                new XElement("Car",
        //                    new XAttribute("Name", x.Name!),
        //                    new XAttribute("Combined", x.Combined),
        //                    new XAttribute("Manufacturer", x.Manufacturer!)))));
        //        document.Save("Resources//Files//fuel.xml");
        //    }
        //    else if (choise == "5")
        //    {
        //        var document = XDocument.Load("Resources//Files//fuel.xml");
        //        var manufacturersXml = document
        //            .Element("Cars")?
        //            .Elements("Car")
        //            .Select(x => x.Attribute("Manufacturer")?.Value).Distinct().Order();

        //        foreach (var manufacturerXml in manufacturersXml!)
        //        {
        //            Console.WriteLine(manufacturerXml);
        //        }
        //    }
        //    else if (choise == "6")
        //    {
        //        var group = manufacturers.GroupJoin(
        //            cars,
        //            x => x.Name,
        //            x => x.Manufacturer,
        //            (manufactuerer, cars) => new
        //            {
        //                Manufacturer = manufactuerer,
        //                Cars = cars
        //            });

        //        foreach (var g in group)
        //        {
        //            Console.WriteLine($"Manufacturer - {g.Manufacturer.Name}, Country: {g.Manufacturer.Country}");
        //            Console.WriteLine($"\tCars - CombinedSum: {g.Cars.Sum(x => x.Combined)}");
        //            foreach (var car in g.Cars)
        //            {
        //                Console.WriteLine($"\t\tCar - {car.Name} Combined: {car.Combined}");
        //            }
        //        }

        //        var document = new XDocument();
        //        var manufacturersXml = new XElement("Manufacturers", group
        //            .Select(x =>
        //                new XElement("Manufacturer",
        //                    new XAttribute("Name", x.Manufacturer.Name!),
        //                    new XAttribute("Country", x.Manufacturer.Country!),
        //                    new XElement("Cars", x.Cars.Select(x =>
        //                        new XElement("Car",
        //                            new XAttribute("Model", x.Name!),
        //                            new XAttribute("Combined", x.Combined))),
        //                        new XAttribute("Country", x.Manufacturer.Country!),
        //                        new XAttribute("CombinedSum", x.Cars.Sum(x => x.Combined)!)))));
        //        document.Add(manufacturersXml);
        //        document.Save("Resources//Files//manufacturersFuel.xml");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Choose 1 or 2 or 3 or 4 or 5 or 6 or Q! No more options!");
        //        Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
        //    }
        //} while (true);
    }
}
