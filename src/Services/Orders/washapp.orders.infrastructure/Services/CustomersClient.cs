using washapp.orders.application.DTO.Contracts;
using washapp.orders.application.Exceptions;
using washapp.orders.application.Services;
using washapp.orders.infrastructure.Grpc.Proto;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;

namespace washapp.orders.infrastructure.Services;

public class CustomersClient : ICustomersClient
{
    private GrpcChannel Channel { get; }
    private Customers.CustomersClient Client { get; }

    public CustomersClient(string grpcServerAddress)
    {
        Channel = GrpcChannel.ForAddress(grpcServerAddress);
        Client = new Customers.CustomersClient(Channel);
    }
    
    public async Task<CustomerToExternalUsageDto> GetCustomer(Guid customerId)
    {
        CustomerReply reply;
        
        try
        {
            reply = await Client.GetCustomerAsync(new CustomerRequest() { CustomerId = customerId.ToString()});
        }
        catch (RpcException e)
        {
            throw new RpcFailException(e.StatusCode, e.Message);
        }

        var deserializedReply = JsonConvert.DeserializeObject<CustomerToExternalUsageDto>(reply.Customer);
        
        return await Task.FromResult(
            deserializedReply
        );
    }
}