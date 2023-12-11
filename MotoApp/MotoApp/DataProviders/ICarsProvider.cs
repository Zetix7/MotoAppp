using MotoApp.Entities;

namespace MotoApp.DataProviders;

public interface ICarsProvider
{
    List<Car> FilterCars(decimal minPrice);
    List<string> GetUniqueColor();
    decimal GetMinimumPriceOfAllCars();
}
