using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Enums;
using MediatR;

namespace washapp.services.customers.application.Commands.Assortments;

public class UpdateCustomerItem : IRequest
{
    public Guid CustomerId { get; set; }
    public Guid AssortmentId { get; set; }
    public string AssortmentName { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public Guid AssortmentCategoryId { get; set; }

    public UpdateCustomerItem(Guid customerId, Guid assortmentId, string assortmentName, double width, double height, double weight, 
        MeasurementUnit measurementUnit, WeightUnit weightUnit, Guid assortmentCategoryId)
    {
        CustomerId = customerId;
        AssortmentId = assortmentId;
        AssortmentName = assortmentName;
        Width = width;
        Height = height;
        Weight = weight;
        MeasurementUnit = measurementUnit;
        WeightUnit = weightUnit;
        AssortmentCategoryId = assortmentCategoryId;
    }
}