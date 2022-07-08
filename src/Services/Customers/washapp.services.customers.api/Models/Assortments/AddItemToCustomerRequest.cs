using System.ComponentModel.DataAnnotations;
using washapp.services.customers.domain.Enums;

namespace washapp.services.customers.api.Models.Assortments;

public class AddItemToCustomerRequest
{
    [Required]
    public Guid AssortmentCategoryId { get; set; }
    [Required]
    public string AssortmentName { get; set; }
    [Required]
    public double Width { get; set; }
    [Required]
    public double Height { get; set; }
    [Required]
    public double Weight { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
    public WeightUnit WeightUnit { get; set; }
}