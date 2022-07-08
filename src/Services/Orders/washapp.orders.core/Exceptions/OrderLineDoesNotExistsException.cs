using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace washapp.orders.core.Exceptions
{
    internal class OrderLineDoesNotExistsException : DomainException
    {
        public override string Code { get; } = nameof(OrderLineDoesNotExistsException)
            .Underscore().Replace("_exception", String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

        public OrderLineDoesNotExistsException():base("Order line does not exists")
        {

        }
    }
}
