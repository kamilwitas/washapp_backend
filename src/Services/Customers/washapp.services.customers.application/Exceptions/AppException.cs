using System.Net;

namespace washapp.services.customers.application.Exceptions;

public abstract class AppException : Exception
{
    public virtual string Code { get; }
    public virtual HttpStatusCode HttpStatusCode { get; }

    public AppException(string message) : base(message)
    {
        
    }
}