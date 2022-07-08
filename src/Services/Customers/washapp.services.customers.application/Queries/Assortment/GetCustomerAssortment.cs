using MediatR;
using washapp.services.customers.application.DTO;

namespace washapp.services.customers.application.Queries.Assortment;

public class GetCustomerAssortment : IRequest<IEnumerable<AssortmentDto>>
{
    public Guid CustomerId { get; set; }
}