using washapp.orders.core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace washapp.orders.application.Commands.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrder>
    {
        private readonly IOrdersRepository _ordersRepository;

        public DeleteOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(DeleteOrder request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId);

            if (order is null)
            {
                return Unit.Value;
            }

            if (order.IsDeletePossible())
            {
                await _ordersRepository.DeleteAsync(order.Id);
            }

            return Unit.Value;
        }
    }
}
