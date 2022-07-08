using washapp.orders.core.Entities;
using washapp.orders.core.Repository;
using MediatR;
using washapp.orders.application.Exceptions;
using washapp.orders.application.Services;

namespace washapp.orders.application.Commands.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrder>
{
    private readonly ICustomersMongoRepository _customersRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUserPrincipalService _userPrincipalService;

    public CreateOrderHandler(ICustomersMongoRepository customersRepository, IOrdersRepository ordersRepository,
        IUserPrincipalService userPrincipalService)
    {
        _customersRepository = customersRepository;
        _ordersRepository = ordersRepository;
        _userPrincipalService = userPrincipalService;
    }

    public async Task<Unit> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetCustomerAsync(request.CustomerId);

        if (customer is null || customer.IsDeleted)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        var userPrincipal = await _userPrincipalService.GetUserPrincipal();


        var newOrder = Order.Create(customer.Id, customer.CompanyName, 
            new Employee(userPrincipal.UserId,userPrincipal.FirstName,userPrincipal.LastName,userPrincipal.Login));

        var assortments = customer.Assortments.ToList();
        
        foreach (var orderLineDto in request.OrderLines)
        {
            var assortment = assortments.FirstOrDefault(x => x.Id == orderLineDto.AssortmentId);

            if (assortment is null)
            {
                throw new AssortmentDoesNotExistsException(orderLineDto.AssortmentId);
            }
            
            newOrder.AddOrderLine(OrderLine.Create(assortment, orderLineDto.Quantity, orderLineDto.TotalWeight, orderLineDto.WeightUnit));
        }
        customer.AddOrder(newOrder.Id);

        await _customersRepository.UpdateAsync(customer);
        await _ordersRepository.AddAsync(newOrder);
        
        return Unit.Value;
    }
}