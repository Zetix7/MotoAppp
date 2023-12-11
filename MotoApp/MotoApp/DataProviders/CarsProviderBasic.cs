using MotoApp.Entities;
using MotoApp.Repositories;

namespace MotoApp.DataProviders;

public class CarsProviderBasic : ICarsProvider
{
    private readonly IRepository<Car> _repository;

    public CarsProviderBasic(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public List<Car> FilterCars(decimal minPrice)
    {
        var list = new List<Car>();
        var cars = _repository.GetAll();

        foreach (var car in cars)
        {
            if(car.Price > minPrice)
            {
                list.Add(car);
            }
        }
        return list;
    }

    public List<string> GetUniqueColor()
    {
        var list = new List<string>();
        var cars = _repository.GetAll();

        foreach (var car in cars)
        {
            if (!list.Contains(car.Color))
            {
                list.Add(car.Color);
            }
        }
        return list;
    }

    public decimal GetMinimumPriceOfAllCars()
    {
        var min = decimal.MaxValue;
        var cars = _repository.GetAll();

        foreach(var car in cars)
        {
            if (min > car.Price)
            {
                min = car.Price;
            }
        }
        return min;
    }
}
