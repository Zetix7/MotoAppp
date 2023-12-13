using MotoApp.Data.Entities;

namespace MotoApp.Components.DataProviders.Extensions;

public static class CarsHelper
{
    public static IEnumerable<Car> ByManufacturer(this IEnumerable<Car> query, string manufacturer)
    {
        return query.Where(x => x.Manufacturer == manufacturer);
    }
}
