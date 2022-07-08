using washapp.orders.application.DTO;
using washapp.orders.application.DTO.Contracts;

namespace washapp.orders.application.Services;

public interface ICustomersClient
{
    Task<CustomerToExternalUsageDto> GetCustomer(Guid customerId);
}