using System.Net;

namespace washapp.services.identity.infrastructure.Exceptions;

public class ExceptionDto
{
    public string ErrorMessage { get; set; }
    public string Code { get; set; }
}