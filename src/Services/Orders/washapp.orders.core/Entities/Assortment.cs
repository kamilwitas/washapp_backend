using washapp.orders.core.Enums;

namespace washapp.orders.core.Entities;

public class Assortment
{
    public Guid Id { get; set; }
    public string AssortmentName { get; set; }
    public double Weight { get; set; }
    public WeightUnit WeightUnit { get; set; }

    public Assortment()
    {
        
    }
    public Assortment(Guid id, string assortmentName, double weight, WeightUnit weightUnit)
    {
        Id = id;
        AssortmentName = assortmentName;
        Weight = weight;
        WeightUnit = weightUnit;
    }

}