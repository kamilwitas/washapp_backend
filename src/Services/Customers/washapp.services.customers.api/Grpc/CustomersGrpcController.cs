using washapp.services.customers.api.Grpc.Protos;
using washapp.services.customers.application.DTO.Contracts.Orders;
using washapp.services.customers.application.Queries.Customers;
using Grpc.Core;
using MediatR;
using Newtonsoft.Json;

namespace washapp.services.customers.api.Grpc;

public class CustomersGrpcController : Customers.CustomersBase
{
    private readonly IMediator _mediator;

    public CustomersGrpcController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CustomerReply> GetCustomer(CustomerRequest request, ServerCallContext context)
    {
        var query = new GetCustomerById() {CustomerId = Guid.Parse(request.CustomerId)};
        var customer = await _mediator.Send(query);

        if (customer is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Customer not found"));
        }
        
        var customerToExternalDto = new CustomerToExternalUsageDto()
        {
            Id = customer.Id,
            CompanyName = customer.CompanyName,
            CustomerColor = customer.CustomerColor,
            LocationId = customer.Address.Location.Id,
            Assortments = customer.AssortmentItems?.Select(x => new AssortmentToExternalUsageDto()
            {
                Id = x.Id,
                AssortmentName = x.AssortmentName,
                Weight = x.Weight,
                WeightUnit = x.WeightUnit

            })
        };

        var serializedResponse = JsonConvert.SerializeObject(customerToExternalDto);

        CustomerReply reply = new()
        {
            Customer = serializedResponse
        };
        
        return reply;
    }
}