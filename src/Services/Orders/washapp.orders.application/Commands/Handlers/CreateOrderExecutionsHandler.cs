using washapp.orders.core.Entities;
using washapp.orders.core.Repository;
using washapp.orders.core.Value_objects.OrderActions;
using MediatR;
using washapp.orders.application.Exceptions;
using washapp.orders.application.Services;

namespace washapp.orders.application.Commands.Handlers
{
    public class CreateOrderExecutionsHandler : IRequestHandler<CreateOrderExecutions>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserPrincipalService _userPrincipalService;

        public CreateOrderExecutionsHandler(IOrdersRepository ordersRepository,
            IUserPrincipalService userPrincipalService)
        {
            _ordersRepository = ordersRepository;
            _userPrincipalService = userPrincipalService;
        }

        public async Task<Unit> Handle(CreateOrderExecutions request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId);

            if (order is null)
            {
                throw new OrderDoesNotExistsException(request.OrderId);
            }

            var userPrincipal = await _userPrincipalService.GetUserPrincipal();
            var employee = new Employee(userPrincipal.UserId, userPrincipal.FirstName, userPrincipal.LastName, userPrincipal.Login);

            foreach (var orderExecution in request.OrderExecutionRequests)
            {
                var orderLine = order.OrderLines?.FirstOrDefault(x=>x.Id==orderExecution.OrderLineId);
                var execution = OrderExecution.Create(employee, orderExecution.OrderLineId,orderExecution.ExecutedQuantity);
                order.AddOrderExecution(execution);
            }

            await _ordersRepository.UpdateAsync(order);
            return Unit.Value;
        }
    }
}
