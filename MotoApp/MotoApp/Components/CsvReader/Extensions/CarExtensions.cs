using MotoApp.Components.CsvReader.Models;
using System.Globalization;

namespace MotoApp.Components.CsvReader.Extensions;

public static class CarExtensions
{
    public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(',');

            yield return new Car
            {
                Year = int.Parse(columns[0], CultureInfo.InvariantCulture),
                Manufacturer = columns[1],
                Name = columns[2],
                Displacement = double.Parse(columns[3], CultureInfo.InvariantCulture),
                Cylinders = int.Parse(columns[4], CultureInfo.InvariantCulture),
                City = int.Parse(columns[5], CultureInfo.InvariantCulture),
                Highway = int.Parse(columns[6], CultureInfo.InvariantCulture),
                Combined = int.Parse(columns[7], CultureInfo.InvariantCulture)
            };
        }
    }
}
