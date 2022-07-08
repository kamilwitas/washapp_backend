using Humanizer;
using System.Net;

namespace washapp.orders.core.Exceptions
{
    public class AssortmentCannotBeNullException : DomainException
    {
        public override string Code { get; } = nameof(AssortmentCannotBeNullException)
            .Underscore().Replace("_exception",String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public AssortmentCannotBeNullException():base("Assortment cannot be null")
        {

        }
    }
}
