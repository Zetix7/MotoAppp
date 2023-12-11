using MotoApp.DataProviders.Extensions;
using MotoApp.Entities;
using MotoApp.Repositories;

namespace MotoApp.DataProviders;

public class CarsProvider : ICarsProvider
{
    private readonly IRepository<Car> _carsRepository;

    public CarsProvider(IRepository<Car> carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public string AnonymousClass()
    {
        var cars = _carsRepository.GetAll();
        var list = cars.Select(x => new
        {
            ProductId = x.Id,
            ProductName = x.Name,
            ProductType = x.Type,
        });

        string s = "";
        foreach (var car in list)
        {
            s += $"Product Id: {car.ProductId}\n";
            s += $"\tProduct Name: {car.ProductName}\n";
            s += $"\tProduct Type: {car.ProductType}\n";
        }
        return s;
    }

    public List<Car> SpecificColumn()
    {
        var cars = _carsRepository.GetAll();
        var list = cars.Select(x => new Car
        {
            Id = x.Id,
            Name = x.Name,
            Type = x.Type
        }).ToList();
        return list;
    }

    public decimal GetMinimumPriceOfAllCars()
    {
        var cars = _carsRepository.GetAll();
        var minimumPrise = cars.Select(x => x.Price).Min();
        return minimumPrise;
    }

    public List<string> GetUniqueColor()
    {
        var cars = _carsRepository.GetAll();
        var colors = cars.Select(x => x.Color).Distinct().ToList();
        return colors;
    }

    public List<Car> OrderByName()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderBy(x => x.Name).ToList();
    }

    public List<Car> OrderByNameDescending()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderByDescending(x => x.Name).ToList();
    }

    public List<Car> OrderByColorAndName()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderBy(x => x.Color).ThenBy(x => x.Name).ToList();
    }

    public List<Car> OrderByColorAndNameDescending()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderByDescending(x => x.Color).ThenByDescending(x => x.Name).ToList();
    }

    public List<Car> WhereStartsWith(string prefix)
    {
        var cars = _carsRepository.GetAll();
        return cars.Where(x => x.Name.StartsWith(prefix)).ToList();
    }

    public List<Car> WhereStartsWithAndPriceGreaterThan(string prefix, decimal price)
    {
        var cars = _carsRepository.GetAll();
        return cars.Where(x => x.Name.StartsWith(prefix) && x.Price > price).ToList();
    }

    public List<Car> WhereColorIs(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.ByColor(color).ToList();
    }

    public Car FirstByColor(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.First(x => x.Color == color);
    }

    public Car? FirstOrDefaultByColor(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.FirstOrDefault(x => x.Color == color);
    }

    public Car? FirstOrDefaultByColorWithDefault(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.FirstOrDefault(x => x.Color == color, new Car { Id = -1, Name = "NOT FOUND", Color = "Default" });
    }

    public Car LastByColor(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.Last(x => x.Color == color);
    }
    public Car SingleById(int id)
    {
        var cars = _carsRepository.GetAll();
        return cars.Single(x => x.Id == id);
    }

    public Car? SingleOrDefaultById(int id)
    {
        var cars = _carsRepository.GetAll();
        return cars.SingleOrDefault(x => x.Id == id);
    }

    public List<Car> TakeCars(int howMany)
    {
        var cars = _carsRepository.GetAll();
        return cars.Take(howMany).ToList();
    }

    public List<Car> TakeCars(Range range)
    {
        var cars = _carsRepository.GetAll();
        return cars.Take(range).ToList();
    }

    public List<Car> TakeCarsWhileNameStartsWith(string prefix)
    {
        var cars = _carsRepository.GetAll();
        return cars.TakeWhile(x=>x.Name.StartsWith(prefix)).ToList();
    }

    public List<Car> SkipCars(int howMany)
    {
        var cars = _carsRepository.GetAll();
        return cars.Skip(howMany).ToList();
    }

    public List<Car> SkipCarsWhileNameStartsWith(string prefix)
    {
        var cars = _carsRepository.GetAll();
        return cars.SkipWhile(x=>x.Name.StartsWith(prefix)).ToList();
    }
}
