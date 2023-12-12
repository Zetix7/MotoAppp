namespace MotoApp.Data.Entities;

public class Car : EntityBase
{
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string Type { get; set; }

    public override string ToString() => $"Car -> id: {Id}, name: {Name}, color: {Color}, price: {Price}, type: {Type}";
}
