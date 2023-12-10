﻿using MotoApp.Entities;
using MotoApp.Repositories;
using System.Text.Json;

namespace MotoApp.Data;

public class DataInFile<T> where T : class, IEntity
{
    public const string FILENAME = ".json";
    private readonly IRepository<T> _repository;

    public DataInFile(IRepository<T> repository)
    {
        FullFileName = $"file{FILENAME}";
        _repository = repository;
    }

    public string FullFileName { get; private set; }

    public event EventHandler<T>? ItemRead;
    public event EventHandler<T>? ItemAdded;

    public void Read()
    {
        if (!File.Exists(FullFileName))
        {
            throw new FileNotFoundException($"File '{FullFileName} not found!");
        }
        else if (new FileInfo(FullFileName).Length == 0)
        {
            throw new FileLoadException($"File '{FullFileName} is empty!");
        }
        else
        {
            foreach (var item in _repository.GetAll())
            {
                _repository.Remove(item);
            }
            _repository.Save();
            using (var reader = File.OpenText(FullFileName))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var item = JsonSerializer.Deserialize<T>(line);
                    foreach (var itemInRepository in _repository.GetAll())
                    {
                        if (item?.Id == itemInRepository.Id)
                        {
                            item.Id++;
                        }
                    }
                    _repository.Add(item!);
                    _repository.Save();
                    line = reader.ReadLine();

                    ItemRead?.Invoke(this, item!);
                }
            }
        }
    }

    public void Add(T item)
    {
        using (var reader = File.OpenText(FullFileName))
        {
            foreach (var itemInRepository in _repository.GetAll())
            {
                _repository.Remove(itemInRepository);
            }
            _repository.Save();
            var line = reader.ReadLine();
            while (line != null)
            {
                var itemFromFile = JsonSerializer.Deserialize<T>(line);
                foreach (var itemInRepository in _repository.GetAll())
                {
                    if (itemFromFile?.Id == itemInRepository.Id)
                    {
                        itemFromFile.Id++;
                    }
                }
                _repository.Add(itemFromFile!);
                _repository.Save();
                line = reader.ReadLine();
            }
        }
        _repository.Add(item!);
        _repository.Save();
        using (var writer = File.CreateText(FullFileName))
        {
            foreach (var itemInRepository in _repository.GetAll())
            {
                writer.WriteLine(JsonSerializer.Serialize(itemInRepository));
                ItemAdded?.Invoke(this, itemInRepository);
            }
        }
    }
}
