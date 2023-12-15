using MotoApp.Components.CsvReader;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

namespace MotoApp.Components.MotoAppStorageAccess;

public class MotoAppStorageAccess : IMotoAppStorageAccess
{
    private readonly ICsvReader _csvReader;
    private readonly IRepository<Car> _carsRepository;
    private readonly IRepository<Manufacturer> _manufacturersRepository;

    public MotoAppStorageAccess(
        ICsvReader csvReader,
        IRepository<Car> carsRepository,
        IRepository<Manufacturer> manufacturersRepository)
    {
        _csvReader = csvReader;
        _carsRepository = carsRepository;
        _manufacturersRepository = manufacturersRepository;
    }

    public void InsertCarsToDatabase()
    {
        var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");

        foreach (var car in cars)
        {
            _carsRepository.Add(new Car
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
        _carsRepository.Save();
    }

    public void InsertManufacturersToDatabase()
    {
        var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");

        foreach (var manufacturer in manufacturers)
        {
            _manufacturersRepository.Add(new Manufacturer
            {
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                Year = manufacturer.Year
            });
        }
        _manufacturersRepository.Save();
    }

    public void ReadCarsFromDatabase()
    {
        foreach (var car in _carsRepository.GetAll())
        {
            Console.WriteLine($"{car.Id,5}. {car.Manufacturer} {car.Name} - Combined: {car.Combined}");
        }
    }

    public void ReadManufacturersFromDatabase()
    {
        foreach (var manufacturer in _manufacturersRepository.GetAll())
        {
            Console.WriteLine($"{manufacturer.Id,5}. | {manufacturer.Name} | {manufacturer.Country} | {manufacturer.Year}");
        }
    }

    public void RemoveCarByNameFromDatabase(string name)
    {
        var car = _carsRepository.GetAll().FirstOrDefault(x => x.Name == name);
        if (car != null)
        {
            _carsRepository.Remove(car!);
            _carsRepository.Save();
        }
    }

    public void RemoveManufacturerByNameFromDatabase(string name)
    {
        var manufacturer = _manufacturersRepository.GetAll().FirstOrDefault(x=>x.Name == name);
        if (manufacturer != null)
        {
            _manufacturersRepository.Remove(manufacturer!);
            _manufacturersRepository.Save();
        }
    }

    public void UpdateCarNameInDatabase(string oldName, string newName)
    {
        var car = _carsRepository.GetAll().FirstOrDefault(x => x.Name == oldName);
        if (car != null)
        {
            car!.Name = newName;
            _carsRepository.Save();
        }
    }

    public void UpdateManufacturerCountryInDatabase(string name, string newCountry)
    {
        var manufacturer = _manufacturersRepository.GetAll().FirstOrDefault(x=>x.Name == name);
        if (manufacturer != null)
        {
            manufacturer!.Country = newCountry;
            _manufacturersRepository.Save();
        }
    }
}
