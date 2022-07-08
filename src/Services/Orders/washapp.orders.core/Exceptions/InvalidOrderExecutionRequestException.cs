using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace washapp.orders.core.Exceptions
{
    public class InvalidOrderExecutionRequestException : DomainException
    {
        public override string Code { get; } = nameof(InvalidOrderExecutionRequestException)
            .Underscore().Replace("_exception",String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public InvalidOrderExecutionRequestException(string message):base(message)
        {

        }
    }
}
