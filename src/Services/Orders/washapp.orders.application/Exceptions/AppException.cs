using System.Net;

namespace washapp.orders.application.Exceptions;

public abstract class AppException : Exception
{
    public abstract string Code { get; }
    public abstract HttpStatusCode HttpStatusCode { get; }

    public AppException(string message):base(message)
    {
        
    }
}