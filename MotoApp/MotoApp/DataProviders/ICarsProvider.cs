using MotoApp.Entities;

namespace MotoApp.DataProviders;

public interface ICarsProvider
{
    List<Car> SpecificColumn();
    List<string> GetUniqueColor();
    decimal GetMinimumPriceOfAllCars();
    string AnonymousClass();

    List<Car> OrderByName();
    List<Car> OrderByNameDescending();
    List<Car> OrderByColorAndName();
    List<Car> OrderByColorAndNameDescending();
}
