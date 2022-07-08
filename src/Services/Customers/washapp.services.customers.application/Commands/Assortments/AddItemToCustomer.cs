using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Enums;
using MediatR;

namespace washapp.services.customers.application.Commands.Assortments;

public class AddItemToCustomer : IRequest
{
    public Guid CustomerId { get; set; }
    public Guid AssortmentCategoryId { get; set; }
    public string AssortmentName { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
    public WeightUnit WeightUnit { get; set; }

    public AddItemToCustomer(Guid customerId, Guid assortmentCategoryId, string assortmentName, double width,
        double height, double weight, MeasurementUnit measurementUnit=MeasurementUnit.Cm, WeightUnit weightUnit=WeightUnit.Kg)
    {
        CustomerId = customerId;
        AssortmentCategoryId = assortmentCategoryId;
        AssortmentName = assortmentName;
        Width = width;
        Height = height;
        Weight = weight;
        MeasurementUnit = measurementUnit;
        WeightUnit = weightUnit;
    }
}