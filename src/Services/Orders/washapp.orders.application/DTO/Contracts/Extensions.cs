using washapp.orders.core.Entities;

namespace washapp.orders.application.DTO.Contracts;

public static class Extensions
{
    public static Customer AsBusiness(this CustomerToExternalUsageDto dto)
    {
        var assortments = dto.Assortments.Select(x => new Assortment(x.Id, x.AssortmentName, x.Weight, x.WeightUnit)).ToList();
        return new Customer(dto.Id, dto.CompanyName, dto.CustomerColor, dto.LocationId, assortments);
    }
    
}