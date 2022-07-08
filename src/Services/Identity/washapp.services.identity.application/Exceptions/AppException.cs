using System.Net;

namespace washapp.services.identity.application.Exceptions;

public abstract class AppException : Exception
{
    public virtual string Code { get; }
    public virtual HttpStatusCode HttpStatusCode { get; }

    public AppException(string message) : base(message)
    {
        
    }
}