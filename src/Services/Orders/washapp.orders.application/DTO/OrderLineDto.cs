using washapp.orders.core.Enums;

namespace washapp.orders.application.DTO;

public class OrderLineDto
{
    public Guid AssortmentId { get; set; }
    public int Quantity { get; set; }
    public double TotalWeight { get; set; }
    public WeightUnit WeightUnit { get; set; } = WeightUnit.Kg;

    public OrderLineDto(Guid assortmentId, int quantity, double totalWeight, WeightUnit weightUnit)
    {
        AssortmentId = assortmentId;
        Quantity = quantity;
        TotalWeight = totalWeight;
        WeightUnit = weightUnit;
    }
}