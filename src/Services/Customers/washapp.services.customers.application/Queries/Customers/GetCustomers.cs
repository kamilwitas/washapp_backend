using washapp.services.customers.domain.Entities;
using MediatR;
using washapp.services.customers.application.DTO;

namespace washapp.services.customers.application.Queries.Customers;

public class GetCustomers : IRequest<IEnumerable<CustomerDto>>
{
    
}