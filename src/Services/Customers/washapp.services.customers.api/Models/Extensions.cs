using washapp.services.customers.application.Commands;
using washapp.services.customers.application.Commands.AssortmentCategories;
using washapp.services.customers.application.Commands.Assortments;
using washapp.services.customers.application.Commands.Customers;
using washapp.services.customers.api.Models.Assortments;
using washapp.services.customers.api.Models.Categories;
using washapp.services.customers.api.Models.Customers;
using washapp.services.customers.api.Models.Locations;
using washapp.services.customers.application.Commands.Locations;

namespace washapp.services.customers.api.Models;

public static class Extensions
{
    public static CreateCategory AsCommand(this CreateCategoryRequest request)
    {
        return new CreateCategory(request.CategoryName);
    }

    public static UpdateCategory AsCommand(this UpdateCategoryRequest request, Guid categoryId)
    {
        return new UpdateCategory()
        {
            CategoryId = categoryId,
            CategoryName = request.CategoryName
        };
    }

    public static CreateCustomer AsCommand(this CreateCustomerRequest request)
    {
        return new CreateCustomer(request.FirstName, request.LastName, request.Street, request.LocalNumber,
            request.PostCode,
            request.LocationId, request.CompanyName, request.CustomerColor);
    }

    public static CreateLocation AsCommand(this CreateLocationRequest request)
    {
        return new CreateLocation(request.LocationName, request.LocationColor);
    }

    public static AddItemToCustomer AsCommand(this AddItemToCustomerRequest request, Guid customerId)
    {
        return new AddItemToCustomer(customerId, request.AssortmentCategoryId, request.AssortmentName, request.Width,
            request.Height, request.Weight, request.MeasurementUnit, request.WeightUnit);
    }

    public static UpdateCustomer AsCommand(this UpdateCustomerRequest request, Guid customerId)
    {
        return new UpdateCustomer(customerId, request.FirstName, request.LastName, request.Street, request.LocalNumber,
            request.PostCode, request.LocationId, request.CompanyName, request.CustomerColor);
    }

    public static UpdateLocation AsCommand(this UpdateLocationRequest request, Guid locationId)
    {
        return new UpdateLocation(locationId, request.LocationName, request.LocationColor);
    }

    public static DeleteCustomerItem AsCommand(this DeleteCustomerItemRequest request, Guid customerId)
    {
        return new DeleteCustomerItem(customerId, request.AssortmentId);
    }

    public static UpdateCustomerItem AsCommand(this UpdateCustomerItemRequest request, Guid customerId)
    {
        return new UpdateCustomerItem(customerId, request.AssortmentId, request.AssortmentName,request.Width,request.Height,
            request.Weight,request.MeasurementUnit,request.WeightUnit,request.AssortmentCategoryId);
    }
}