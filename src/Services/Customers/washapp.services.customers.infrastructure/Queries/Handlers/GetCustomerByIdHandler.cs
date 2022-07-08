using washapp.services.customers.application.Exceptions;
using washapp.services.customers.application.Queries.Customers;
using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using MediatR;

namespace washapp.services.customers.infrastructure.Queries.Handlers;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById,Customer>
{
    private readonly ICustomersRepository _customersRepository;

    public GetCustomerByIdHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<Customer> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetAsync(request.CustomerId);

        if (customer is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        return customer;
    }
}