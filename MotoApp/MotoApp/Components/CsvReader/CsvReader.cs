using MotoApp.Components.CsvReader.Extensions;
using MotoApp.Components.CsvReader.Models;
using System.Globalization;

namespace MotoApp.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {
        public List<Car> ProcessCars(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Car>();
            }

            var cars = File.ReadAllLines(filePath)
                .Skip(1).Where(x => x.Length > 1).ToCar().ToList();
                //.Select(x => x.Split(','))
                //.Select(x => new Car
                //{
                //    Year = int.Parse(x[0], CultureInfo.InvariantCulture),
                //    Manufacturer = x[1],
                //    Name = x[2],
                //    Displacement = double.Parse(x[3], CultureInfo.InvariantCulture),
                //    Cylinders = int.Parse(x[4], CultureInfo.InvariantCulture),
                //    City = int.Parse(x[5], CultureInfo.InvariantCulture),
                //    Highway = int.Parse(x[6], CultureInfo.InvariantCulture),
                //    Combined = int.Parse(x[7], CultureInfo.InvariantCulture),
                //}).ToList();
            return cars;
        }
    }
}