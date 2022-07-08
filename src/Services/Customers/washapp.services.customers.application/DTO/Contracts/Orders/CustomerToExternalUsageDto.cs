namespace washapp.services.customers.application.DTO.Contracts.Orders;

public class CustomerToExternalUsageDto
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string CustomerColor { get; set; }
    public Guid LocationId { get; set; }
    public IEnumerable<AssortmentToExternalUsageDto>Assortments { get; set; }
}