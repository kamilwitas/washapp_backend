using washapp.orders.core.Enums;

namespace washapp.orders.application.DTO.Contracts;

public class AssortmentToExternalUsageDto
{
    public Guid Id { get; set; }
    public string AssortmentName { get; set; }
    public double Weight { get; set; }
    public WeightUnit WeightUnit { get; set; }
}