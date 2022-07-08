using washapp.services.customers.domain.Entities;
using MediatR;

namespace washapp.services.customers.application.Queries.Customers;

public class GetCustomerById : IRequest<Customer>
{
    public Guid CustomerId { get; set; }
}