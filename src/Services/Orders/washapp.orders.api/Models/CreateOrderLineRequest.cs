using System.ComponentModel.DataAnnotations;
using washapp.orders.core.Enums;

namespace washapp.orders.api.Models;

public class CreateOrderLineRequest
{
    [Required]
    public Guid AssortmentId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double TotalWeight { get; set; }
    public WeightUnit WeightUnit { get; set; } = WeightUnit.Kg;
}