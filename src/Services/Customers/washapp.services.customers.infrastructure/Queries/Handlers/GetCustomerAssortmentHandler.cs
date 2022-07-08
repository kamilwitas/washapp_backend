using washapp.services.customers.application.DTO;
using washapp.services.customers.application.Exceptions;
using washapp.services.customers.application.Mappings;
using washapp.services.customers.application.Queries.Assortment;
using washapp.services.customers.domain.Repository;
using MediatR;
using washapp.services.customers.domain.Repository;

namespace washapp.services.customers.infrastructure.Queries.Handlers;

public class GetCustomerAssortmentHandler : IRequestHandler<GetCustomerAssortment, IEnumerable<AssortmentDto>>
{
    private readonly ICustomersRepository _customersRepository;

    public GetCustomerAssortmentHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<IEnumerable<AssortmentDto>> Handle(GetCustomerAssortment request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetAsync(request.CustomerId);

        if (customer is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        var assortmentDtoCollection = customer.AssortmentItems?.Select(x => x.AsDto());
        return assortmentDtoCollection;
    }
}