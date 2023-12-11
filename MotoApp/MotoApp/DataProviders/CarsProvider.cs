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
        return cars.OrderBy(x=>x.Name).ToList();
    }

    public List<Car> OrderByNameDescending()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderByDescending(x=>x.Name).ToList();
    }

    public List<Car> OrderByColorAndName()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderBy(x=>x.Color).ThenBy(x=>x.Name).ToList();
    }

    public List<Car> OrderByColorAndNameDescending()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderByDescending(x => x.Color).ThenByDescending(x => x.Name).ToList();
    }
}
