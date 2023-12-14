using MotoApp.Components.CsvReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;
using MotoApp.Data.Repositories.Extensions;

namespace MotoApp.Components.MotoAppStorageAccess;

public class MotoAppStorageAccess : IMotoAppStorageAccess
{
    private readonly ICsvReader _csvReader;
    private readonly MotoAppDbContext _motoAppDbContext;

    public MotoAppStorageAccess(
        ICsvReader csvReader,
        MotoAppDbContext motoAppDbContext)
    {
        _csvReader = csvReader;
        _motoAppDbContext = motoAppDbContext;
        _motoAppDbContext.Database.EnsureCreated();
    }

    public void InsertCarsToDatabase()
    {
        var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");

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

    public void InsertManufacturersToDatabase()
    {
        var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");

        foreach (var manufacturer in manufacturers)
        {
            _motoAppDbContext.Manufacturers.Add(new Manufacturer
            {
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                Year = manufacturer.Year
            });
        }
        _motoAppDbContext.SaveChanges();
    }

    public void ReadCarsFromDatabase()
    {
        var cars = _motoAppDbContext.Cars.ToList();

        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Id:5} {car.Manufacturer} {car.Name} - Combined: {car.Combined}");
        }
    }

    public void ReadManufacturersFromDatabase()
    {
        var manufacturers = _motoAppDbContext.Manufacturers.ToList();

        foreach(var manufacturer in manufacturers)
        {
            Console.WriteLine($"{manufacturer.Name} {manufacturer.Country} {manufacturer.Year}");
        }
    }

    public void RemoveCarByNameFromDatabase(string name)
    {
        var car = _motoAppDbContext.Cars.SingleOrDefault(x => x.Name == name);
        _motoAppDbContext.Cars.Remove(car!);
        _motoAppDbContext.SaveChanges();
    }

    public void RemoveManufacturerByNameFromDatabase(string name)
    {
        var manufacturer = _motoAppDbContext.Manufacturers.SingleOrDefault(x=>x.Name == name);
        _motoAppDbContext.Manufacturers.Remove(manufacturer!);
        _motoAppDbContext.SaveChanges();
    }

    public void UpdateCarNameInDatabase(string oldName, string newName)
    {
        var car = _motoAppDbContext.Cars.SingleOrDefault(x=> x.Name == oldName);
        car!.Name = newName;
        _motoAppDbContext.SaveChanges();
    }

    public void UpdateManufacturerCountryInDatabase(string oldCountry, string newCountry)
    {
        var manufacturer = _motoAppDbContext.Manufacturers.SingleOrDefault(x => x.Country == oldCountry);
        manufacturer!.Country = newCountry;
        _motoAppDbContext.SaveChanges();
    }
}
