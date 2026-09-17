using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Carbon.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Middlewares;

public class ExceptionHandlerMiddleware : IExceptionHandler
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        const string contentType = "application/problem+json";

        var errorCode = exception is DomainException domainException
            ? (int)domainException.StatusCode
            : StatusCodes.Status500InternalServerError;
        
        httpContext.Response.ContentType = contentType;
        httpContext.Response.StatusCode = errorCode;
        
        var problemDetails = CreateProblemDetails(httpContext, exception);
        await httpContext.Response.WriteAsJsonAsync(problemDetails, SerializerOptions, cancellationToken);
        
        return true;
    }
    
    private static ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var statusCode = context.Response.StatusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = exception.GetType().FullName ?? string.Empty,
            Extensions =
            {
                ["traceId"] = Activity.Current?.Id,
                ["requestId"] = context.TraceIdentifier,
                ["data"] = exception.Data
            },
            Detail = exception.Message
        };

        return problemDetails;
    }
}