using MotoApp.DataProviders;
using MotoApp.Entities;
using MotoApp.Repositories;
using MotoApp.Repositories.Extensions;

namespace MotoApp;

public class CarApp : ICarApp
{
    private readonly IRepository<Car> _carRepository;
    private readonly ICarsProvider _carsProvider;

    public CarApp(IRepository<Car> carRepository, ICarsProvider carsProvider)
    {
        _carRepository = carRepository;
        _carsProvider = carsProvider;
    }

    public void ShowDataOfCarsExample()
    {
        var cars = GenerateSampleCars();
        _carRepository.AddBatch(cars);

        foreach (var car in _carsProvider.SpecificColumn())
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var color in _carsProvider.GetUniqueColor())
        {
            Console.WriteLine(color);
        }

        Console.WriteLine("\t" + _carsProvider.GetMinimumPriceOfAllCars());
        Console.WriteLine(_carsProvider.AnonymousClass());

        foreach (var car in _carsProvider.OrderByName())
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var car in _carsProvider.OrderByNameDescending())
        {
            Console.WriteLine(car);
        }

        foreach (var car in _carsProvider.OrderByColorAndName())
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var car in _carsProvider.OrderByColorAndNameDescending())
        {
            Console.WriteLine(car);
        }

        foreach (var car in _carsProvider.WhereStartsWith("Audi R"))
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var car in _carsProvider.WhereStartsWithAndPriceGreaterThan("Audi R", 500000))
        {
            Console.WriteLine(car);
        }

        foreach (var car in _carsProvider.WhereColorIs("Green"))
        {
            Console.WriteLine("\t" + car);
        }

        Console.WriteLine(_carsProvider.FirstByColor("Green"));

        Console.WriteLine("\t" + _carsProvider.FirstOrDefaultByColor("DarkGreen"));

        Console.WriteLine(_carsProvider.FirstOrDefaultByColorWithDefault("Purple"));

        Console.WriteLine("\t" + _carsProvider.LastByColor("White"));

        Console.WriteLine(_carsProvider.SingleById(7));

        Console.WriteLine("\t" + _carsProvider.SingleOrDefaultById(77));

        foreach (var car in _carsProvider.TakeCars(7))
        {
            Console.WriteLine(car);
        }

        foreach (var car in _carsProvider.TakeCars(2..7))
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var car in _carsProvider.TakeCarsWhileNameStartsWith("Audi A"))
        {
            Console.WriteLine(car);
        }

        foreach (var car in _carsProvider.SkipCars(21))
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var car in _carsProvider.SkipCarsWhileNameStartsWith("Audi A"))
        {
            Console.WriteLine(car);
        }

        foreach (var car in _carsProvider.DistinctAllColors())
        {
            Console.WriteLine("\t" + car);
        }

        foreach (var car in _carsProvider.DistinctByColors())
        {
            Console.WriteLine(car);
        }

        foreach (var chunk in _carsProvider.ChunkCars(5))
        {
            foreach (var car in chunk)
            {
                Console.WriteLine("\t" + car);
            }
            Console.WriteLine();
        }
    }

    public List<Car> GenerateSampleCars()
    {
        return new List<Car>(){
            new Car { Id = 100, Name = "Audi A1", Color="White", Price=100000, Type="Compact" },
            new Car { Id = 200, Name = "Audi A2", Color="Blue", Price=150000, Type="Compact" },
            new Car { Id = 300, Name = "Audi A3", Color="Black", Price=200000, Type="Sportback" },
            new Car { Id = 400, Name = "Audi A4", Color="Silver", Price=250000, Type="Avant" },
            new Car { Id = 500, Name = "Audi A5", Color="Red", Price=300000, Type="Liftback" },
            new Car { Id = 600, Name = "Audi A6", Color="Green", Price=400000, Type="Limusine" },
            new Car { Id = 700, Name = "Audi A7", Color="Pink", Price=500000, Type="Liftback" },
            new Car { Id = 800, Name = "Audi A8", Color="Orange", Price=650000, Type="Limusine" },
            new Car { Id = 900, Name = "Audi TT", Color="Silver", Price=230000, Type="Coupe" },

            new Car { Id = 101, Name = "Audi S1", Color="Blue", Price=120000, Type="Compact" },
            new Car { Id = 201, Name = "Audi S2", Color="Green", Price=180000, Type="Compact" },
            new Car { Id = 301, Name = "Audi S3", Color="Pink", Price=250000, Type="Limusine" },
            new Car { Id = 401, Name = "Audi S4", Color="Yellow", Price=300000, Type="Avant" },
            new Car { Id = 501, Name = "Audi S5", Color="Red", Price=350000, Type="Liftback" },
            new Car { Id = 601, Name = "Audi S6", Color="Black", Price=450000, Type="Avant" },
            new Car { Id = 701, Name = "Audi S7", Color="Green", Price=550000, Type="Liftback" },
            new Car { Id = 801, Name = "Audi S8", Color="Silver", Price=690000, Type="Limusine" },
            new Car { Id = 901, Name = "Audi TTS", Color="Red", Price=280000, Type="Coupe" },

            new Car { Id = 302, Name = "Audi RS3", Color="Pink", Price=300000, Type="Limusine" },
            new Car { Id = 402, Name = "Audi RS4", Color="Yellow", Price=400000, Type="Avant" },
            new Car { Id = 502, Name = "Audi RS5", Color="Red", Price=500000, Type="Liftback" },
            new Car { Id = 602, Name = "Audi RS6", Color="Black", Price=620000, Type="Avant" },
            new Car { Id = 702, Name = "Audi RS7", Color="Green", Price=740000, Type="Liftback" },
            new Car { Id = 802, Name = "Audi R8", Color="Silver", Price=870000, Type="Coupe" },
            new Car { Id = 902, Name = "Audi TTRS", Color="Black", Price=330000, Type="Coupe" },
        };
    }
}
