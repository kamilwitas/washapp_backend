using washapp.services.customers.application.DTO;
using washapp.services.customers.application.Mappings;
using washapp.services.customers.application.Queries.Customers;
using washapp.services.customers.domain.Repository;
using MediatR;

namespace washapp.services.customers.infrastructure.Queries.Handlers;

public class GetCustomersByLocationHandler : IRequestHandler<GetCustomersByLocation, IEnumerable<CustomerDto>>
{
    private readonly ICustomersRepository _customersRepository;

    public GetCustomersByLocationHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersByLocation request, CancellationToken cancellationToken)
    {
        var customers = await _customersRepository.GetByLocationAsync(request.LocationId);

        return customers.Select(x => x?.AsDto());
    }
}