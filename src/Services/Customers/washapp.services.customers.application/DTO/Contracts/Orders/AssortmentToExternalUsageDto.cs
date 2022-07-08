using washapp.services.customers.domain.Enums;

namespace washapp.services.customers.application.DTO.Contracts.Orders;

public class AssortmentToExternalUsageDto
{
    public Guid Id { get; set; }
    public string AssortmentName { get; set; }
    public double Weight { get; set; }
    public WeightUnit WeightUnit { get; set; }
}