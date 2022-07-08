using MediatR;

namespace washapp.services.customers.application.Commands.Customers;

public class DeleteCustomer : IRequest
{
    public Guid CustomerId { get; set; }
}