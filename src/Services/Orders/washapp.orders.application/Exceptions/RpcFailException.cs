using Grpc.Core;
using System.Net;

namespace washapp.orders.application.Exceptions;

public class RpcFailException : AppException
{
    public override string Code { get; } = "rpc_exception";
    public override HttpStatusCode HttpStatusCode { get; }

    public RpcFailException(StatusCode code, string message) : base(message)
    {
        switch (code)
        {
            case StatusCode.NotFound: HttpStatusCode = HttpStatusCode.NotFound;
                break;
            default: HttpStatusCode = HttpStatusCode.InternalServerError;
                break;
        };
    }
}