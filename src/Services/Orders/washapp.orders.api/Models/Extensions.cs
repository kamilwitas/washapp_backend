using washapp.orders.application.Commands;
using washapp.orders.application.DTO;
using washapp.orders.application.Queries;

namespace washapp.orders.api.Models;

public static class Extensions
{
    public static CreateOrder AsCommand(this CreateOrderRequest request, Guid customerId)
    {
        return new CreateOrder(customerId,
            request.OrderLines.Select(x => new OrderLineDto(x.AssortmentId, x.Quantity, x.TotalWeight, x.WeightUnit)));
    }

    public static GetAllPagedOrders AsCommand(this GetAllPagedOrdersRequest request, int pageNumber, int pageSize)
    {
        return new GetAllPagedOrders(request.ComapnyNameSearchPhrase, pageNumber, pageSize,
            request.SortBy, request.SortDirection);
    }

    public static CreateOrderExecutions AsCommand(this CreateOrderExecutionsRequest request, Guid orderId)
    {
        return new CreateOrderExecutions(orderId, request.OrderExecutionRequests);
    }

}