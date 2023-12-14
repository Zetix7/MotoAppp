namespace MotoApp.Components.MotoAppStorageAccess;

public interface IMotoAppStorageAccess
{
    void InsertCarsToDatabase();
    void InsertManufacturersToDatabase();
    void ReadCarsFromDatabase();
    void ReadManufacturersFromDatabase();
    void UpdateCarNameInDatabase(string oldName, string newName);
    void UpdateManufacturerCountryInDatabase(string oldCountry, string newCountry);
    void RemoveCarByNameFromDatabase(string name);
    void RemoveManufacturerByNameFromDatabase(string name);
}
