using System.ComponentModel.DataAnnotations;
using washapp.services.customers.domain.Enums;

namespace washapp.services.customers.api.Models.Assortments;

public class UpdateCustomerItemRequest
{
    [Required]
    public Guid AssortmentId { get; set; }
    [Required]
    public string AssortmentName { get; set; }
    [Required]
    public double Width { get; set; }
    [Required]
    public double Height { get; set; }
    [Required]
    public double Weight { get; set; }
    [Required]
    public MeasurementUnit MeasurementUnit { get; set; }
    [Required]
    public WeightUnit WeightUnit { get; set; }
    [Required]
    public Guid AssortmentCategoryId { get; set; }

    public UpdateCustomerItemRequest(Guid assortmentId, string assortmentName, double width, double height, 
        double weight, MeasurementUnit measurementUnit, WeightUnit weightUnit, Guid assortmentCategoryId)
    {
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