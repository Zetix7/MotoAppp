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
}
