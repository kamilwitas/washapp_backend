using System.Net;

namespace washapp.orders.infrastructure.Exceptions;

public class ExceptionDto
{
    public string ErrorMessage { get; set; }
    public string Code { get; set; }
}