using washapp.orders.core.Enums;
using washapp.orders.core.Exceptions;

namespace washapp.orders.core.Entities;

public class OrderLine
{
    public Guid Id { get; set; }
    public Assortment Assortment { get; set; }
    public int Quantity { get; set; }
    public double TotalWeight { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public bool IsExecuted { get; set; }

    public OrderLine()
    {
    }

    public OrderLine(Guid orderLineId,Assortment assortment, int quantity, double totalWeight, WeightUnit weightUnit,
        bool isExecuted=false)
    {
        Id = orderLineId;
        Assortment = assortment;
        Quantity = quantity;
        TotalWeight = totalWeight;
        WeightUnit = weightUnit;
        IsExecuted = isExecuted;
    }

    public static OrderLine Create(Assortment assortment, int quantity, double totalWeight, WeightUnit weightUnit,
        bool isExecuted=false)
    {
        if (quantity <= 0)
        {
            throw new InvalidOrderLineQuantityException();
        }

        if (assortment is null)
        {
            throw new AssortmentCannotBeNullException();
        }

        return new OrderLine(Guid.NewGuid(),assortment, quantity, totalWeight, weightUnit,isExecuted);
    }

    public void CloseOrderLine()
    {
        IsExecuted = true;
    }
}