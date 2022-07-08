using washapp.services.customers.application.DTO;
using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.application.Mappings;

public static class DtoMappingsExtensions
{
    public static CustomerDto AsDto(this Customer customer)
    {
        return new CustomerDto(customer.Id,customer.FirstName, customer.LastName, customer.Address.Location.LocationName,
            customer.Address.Street, customer.Address.LocalNumber, customer.Address.PostCode, customer.CompanyName, customer.CustomerColor, 
            customer.Address.Location.Id);
    }

    public static AssortmentDto AsDto(this Assortment assortment)
    {
        return new AssortmentDto(assortment.Id, assortment.AssortmentName, assortment.Width, assortment.Height,
            assortment.Weight,
            assortment.MeasurementUnit.ToString(), assortment.WeightUnit.ToString(), assortment.AssortmentCategory.CategoryName,
            assortment.AssortmentCategory.Id);
    }
}