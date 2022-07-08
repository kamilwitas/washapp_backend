using MediatR;
using washapp.services.customers.application.DTO;

namespace washapp.services.customers.application.Queries.Customers;

public class GetCustomersByLocation : IRequest<IEnumerable<CustomerDto>>
{
    public Guid LocationId { get; set; }

    public GetCustomersByLocation(Guid locationId)
    {
        LocationId = locationId;
    }
}