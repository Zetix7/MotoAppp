﻿using Microsoft.EntityFrameworkCore;
using MotoApp.Data.Entities;

namespace MotoApp.Data;

public class MotoAppDbContext : DbContext
{
    public MotoAppDbContext(DbContextOptions<MotoAppDbContext> options): base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.LogTo(Console.WriteLine);
    //}
}
