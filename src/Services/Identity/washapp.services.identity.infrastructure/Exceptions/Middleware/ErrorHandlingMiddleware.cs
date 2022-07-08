using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using washapp.services.identity.application.Exceptions;
using washapp.services.identity.domain.Exceptions.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace washapp.services.identity.infrastructure.Exceptions.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (DomainException domainException)
        {
            _logger.LogError($"{nameof(DomainException)}: {domainException.Message} with error code: {domainException.Code}");
            context.Response.StatusCode = (int) domainException.HttpStatusCode;
            var responseJson=JsonConvert.SerializeObject(ExceptionToResponseMapper.Map(domainException));
            context.Response.ContentType = "text/json";
            await context.Response.WriteAsync(responseJson);
        }
        catch (AppException appException)
        {
            _logger.LogError($"{nameof(AppException)}: {appException.Message} with error code: {appException.Code}");
            context.Response.StatusCode = (int) appException.HttpStatusCode;
            var responseJson=JsonConvert.SerializeObject(ExceptionToResponseMapper.Map(appException));
            context.Response.ContentType = "text/json";
            await context.Response.WriteAsync(responseJson);

        }
            
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(e.Message + "\n" + e.StackTrace);   
        }

    }
}