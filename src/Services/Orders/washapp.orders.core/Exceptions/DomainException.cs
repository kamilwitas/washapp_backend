using System.Net;

namespace washapp.orders.core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string Code { get; }
        public abstract HttpStatusCode HttpStatusCode { get; }

        public DomainException(string message):base(message)
        {

        }
        
    }
}
