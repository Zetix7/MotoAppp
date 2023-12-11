﻿using MotoApp.Entities;

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

    List<Car> WhereStartsWith(string prefix);
    List<Car> WhereStartsWithAndPriceGreaterThan(string prefix, decimal price);
    List<Car> WhereColorIs(string color);

    Car FirstByColor(string color);
    Car? FirstOrDefaultByColor(string color);
    Car? FirstOrDefaultByColorWithDefault(string color);
    Car LastByColor(string color);
    Car SingleById(int id);
    Car? SingleOrDefaultById(int id);

    List<Car> TakeCars(int howMany);
    List<Car> TakeCars(Range range);
    List<Car> TakeCarsWhileNameStartsWith(string prefix);

    List<Car> SkipCars(int howMany);
    List<Car> SkipCarsWhileNameStartsWith(string prefix);

}
