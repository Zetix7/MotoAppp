using MotoApp.Components.CsvReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
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

        do
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Choose one option.");
            Console.WriteLine("\t1 - GroupBy");
            Console.WriteLine("\t2 - Join");
            Console.WriteLine("\t3 - GroupJoin");
            Console.WriteLine("\t4 - Create fuel.xml file");
            Console.WriteLine("\t5 - Read fuel.xml file");
            Console.WriteLine("\t6 - Print datas and create manufacturersFuel.xml file");
            Console.WriteLine("\t7 - Insert Cars data to MotoAppStorage database (NOW COMMENTED!!)");
            Console.WriteLine("\t8 - Read Cars data from MotoAppStorage database");
            Console.WriteLine("\t9 - Update car Name data in MotoAppStorage database");
            Console.WriteLine("\t10 - Remove car from MotoAppStorage database");
            Console.WriteLine("\tQ - Exit");
            Console.Write("\t\tYour choise: ");
            var choise = Console.ReadLine();

            if (choise == "q" || choise == "Q")
            {
                break;
            }
            else if (choise == "1")
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
            else if (choise == "4")
            {
                //InsertDataToDatabase(cars);
            }
            else if (choise == "5")
            {
                ReadCarsFromMotoAppStorageAndPrintResult();
            }
            else if (choise == "6")
            {
                UpdateCarNameInMotoAppStorage();
            }
            else if (choise == "7")
            {
                RemoveCarFromMotoAppStorage();
            }
            else
            {
                Console.WriteLine("Choose just  one from 1 to 9 or Q! No more options!");
                Console.WriteLine("\tIf you do not choose, You will stuck here forever!");
            }
        } while (true);
    }

    private void RemoveCarFromMotoAppStorage()
    {
        var alfaRomeo = _motoAppDbContext.Cars.FirstOrDefault(x => x.Manufacturer == "ALFA ROMEO");
        _motoAppDbContext.Cars.Remove(alfaRomeo!);
        _motoAppDbContext.SaveChanges();
    }

    private void UpdateCarNameInMotoAppStorage()
    {
        var alfaRomeo = _motoAppDbContext.Cars.FirstOrDefault(x => x.Manufacturer == "ALFA ROMEO");
        alfaRomeo!.Name = "4C";
        _motoAppDbContext.SaveChanges();
    }

    private void ReadCarsFromMotoAppStorageAndPrintResult()
    {
        var groups = _motoAppDbContext.Cars
                            .GroupBy(x => x.Manufacturer)
                            .Select(x => new 
                            {
                                Manufacturer = x.Key,
                                Cars = x.ToList(),
                            })
                            .OrderBy(x => x.Manufacturer)
                            .ToList();

        foreach (var group in groups)
        {
            Console.WriteLine($"\t{group.Manufacturer} - Combined: {group.Cars.Sum(x=>x.Combined)}");
        }
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

    private void InsertDataToDatabase(List<CsvReader.Models.Car> cars)
    {
        foreach (var car in cars)
        {
            _motoAppDbContext.Cars.Add(new Car
            {
                Year = car.Year,
                Manufacturer = car.Manufacturer,
                Name = car.Name,
                Displacement = car.Displacement,
                Cylinders = car.Cylinders,
                City = car.City,
                Highway = car.Highway,
                Combined = car.Combined
            });
        }
        _motoAppDbContext.SaveChanges();
    }
}
