using washapp.orders.application.Exceptions;
using washapp.orders.core.Exceptions;

namespace washapp.orders.infrastructure.Exceptions;

public static class ExceptionToResponseMapper
{
    public static ExceptionDto Map(AppException appException)
    {
        return new ExceptionDto()
        {
            ErrorMessage = appException.Message,
            Code = appException.Code
        };
    }
    public static ExceptionDto Map(DomainException domainException)
    {
        return new ExceptionDto()
        {
            ErrorMessage = domainException.Message,
            Code = domainException.Code
        };
    }


}