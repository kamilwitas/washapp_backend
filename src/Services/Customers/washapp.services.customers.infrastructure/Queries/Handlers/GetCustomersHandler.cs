using washapp.services.customers.application.DTO;
using washapp.services.customers.application.Mappings;
using washapp.services.customers.application.Queries.Customers;
using washapp.services.customers.domain.Repository;
using MediatR;

namespace washapp.services.customers.infrastructure.Queries.Handlers;

public class GetCustomersHandler : IRequestHandler<GetCustomers,IEnumerable<CustomerDto>>
{
    private readonly ICustomersRepository _customersRepository;

    public GetCustomersHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        var customers = await _customersRepository.GetAll();

        return customers?.Select(x => x?.AsDto());

    }
}