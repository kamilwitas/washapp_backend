using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace washapp.orders.application.Commands
{
    public class DeleteOrder : IRequest
    {
        public Guid OrderId { get; }

        public DeleteOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
