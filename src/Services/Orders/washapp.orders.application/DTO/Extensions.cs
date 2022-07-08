using washapp.orders.core.Entities;

namespace washapp.orders.application.DTO;

public static class Extensions
{
    public static OrderDetailsDto AsDetailsDto(this Order entity)
    {
        var orderLinesDetailsDto = entity.OrderLines.Select(x => 
            new OrderLineDetailsDto(x.Id,x.Assortment.Id,x.Assortment.AssortmentName,
                x.Assortment.Weight,x.Assortment.WeightUnit,x.Quantity,x.TotalWeight,x.WeightUnit));

        return new OrderDetailsDto(entity.Id, entity.OrderState, entity.CustomerId,entity.CompanyName,
            orderLinesDetailsDto,entity.CreatedBy.AsDto());
    }

    public static OrderDto AsDto(this Order entity)
    {
        return new OrderDto(entity.Id, entity.OrderState.ToString(), entity.CustomerId, entity.CompanyName, 
            entity.CreatedAt);
    }

    public static EmployeeDto AsDto(this Employee entity)
    {
        return new EmployeeDto(entity.Id, entity.FirstName, entity.LastName, entity.Login);
    }
}