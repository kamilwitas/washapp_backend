using washapp.orders.core.Enums;

namespace washapp.orders.application.DTO;

public class OrderLineDetailsDto
{
    public Guid OrderLineId { get; set; }
    public Guid AssortmentId { get; set; }
    public string AssortmentName { get; set; }
    public double CatalogueWeight { get; set; }
    public WeightUnit CatalogueWeightUnit { get; set; }
    public int Quantity { get; set; }
    public double RealTotalWeight { get; set; }
    public WeightUnit RealWeightUnit { get; set; }

    public OrderLineDetailsDto(Guid orderLineId, Guid assortmentId, string assortmentName, double catalogueWeight, 
        WeightUnit catalogueWeightUnit, int quantity, double realTotalWeight, WeightUnit realWeightUnit)
    {
        OrderLineId = orderLineId;
        AssortmentId = assortmentId;
        AssortmentName = assortmentName;
        CatalogueWeight = catalogueWeight;
        CatalogueWeightUnit = catalogueWeightUnit;
        Quantity = quantity;
        RealTotalWeight = realTotalWeight;
        RealWeightUnit = realWeightUnit;
    }
}