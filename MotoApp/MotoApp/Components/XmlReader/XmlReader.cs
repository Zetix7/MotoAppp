using MotoApp.Components.CsvReader;
using System.Xml.Linq;

namespace MotoApp.Components.XmlReader;

public class XmlReader : IXmlReader
{
    private readonly ICsvReader _csvReader;

    public XmlReader(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }

    public void CreateFuelXmlFile()
    {
        var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");

        var xmlFile = new XElement("Cars", cars
            .Select(x => new XElement("Car",
                new XAttribute("Manufacturer", x.Manufacturer!),
                new XAttribute("Name", x.Name!),
                new XAttribute("Combined", x.Combined))));

        var document = new XDocument();
        document.Add(xmlFile);
        document.Save("Resources//Files//fuel.xml");
    }

    public void CreateManufacturersFuelXmlFile()
    {
        var cars = _csvReader.ProcessCars("Resources//Files//fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");

        var manufacturersFuel = manufacturers.GroupJoin(
            cars,
            x => x.Name,
            x => x.Manufacturer,
            (m, c) => new
            {
                Manufacturer = m,
                Cars = c,
            });

        var xmlFile = new XElement("Manufacturers", manufacturersFuel.Select(x =>
            new XElement("Manufacturer",
                new XAttribute("Name", x.Manufacturer.Name!),
                new XAttribute("Country", x.Manufacturer.Country!),
                new XElement("Cars", x.Cars.Select(x =>
                    new XElement("Car",
                        new XAttribute("Model", x.Name!),
                        new XAttribute("Combined", x.Combined))),
                    new XAttribute("Country", x.Manufacturer.Country!),
                    new XAttribute("CombinedSum", x.Cars.Sum(x => x.Combined))))));

        var document = new XDocument();
        document.Add(xmlFile);
        document.Save("Resources//Files//manufacturersFuel.xml");
    }

    public void CreateManufacturerXmlFile()
    {
        var manufacturers = _csvReader.ProcessManufacturers("Resources//Files//manufacturers.csv");

        var xmlFile = new XElement("Manufacturers", manufacturers
            .Select(x => new XElement("Manufacturer",
                new XAttribute("Name", x.Name!),
                new XAttribute("Country", x.Country!),
                new XAttribute("Year", x.Year))));

        var document = new XDocument();
        document.Add(xmlFile);
        document.Save("Resources//Files//manufacturers.xml");
    }

    public void ReadFuelXmlFile()
    {
        var xmlFile = XDocument.Load("Resources//Files//fuel.xml");

        var cars = xmlFile.Element("Cars")?
            .Elements("Cars")
            .Select(x => new
            {
                Manufacturer = x.Attribute("Manufacturer")?.Value,
                Name = x.Attribute("Name")?.Value,
                Combined = x.Attribute("Combined")?.Value,
            });

        foreach(var car in cars!)
        {
            Console.WriteLine($"{car.Manufacturer} {car.Name}");
            Console.WriteLine($"\tCombined: {car.Combined}");
        }
    }

    public void ReadManufacturerXmlFile()
    {
        var xmlFile = XDocument.Load("Resources//Files//manufacturers.xml");

        var manufacturers = xmlFile.Element("Manufacturers")?
            .Elements("Manufacturer")
            .Select(x => new
            {
                Name = x.Attribute("Name")?.Value,
                Country = x.Attribute("Country")?.Value,
                Year = x.Attribute("Year")?.Value
            });

        foreach (var manufacturer in manufacturers!)
        {
            Console.WriteLine($"{manufacturer.Name} {manufacturer.Country} {manufacturer.Year}");
        }
    }
}
