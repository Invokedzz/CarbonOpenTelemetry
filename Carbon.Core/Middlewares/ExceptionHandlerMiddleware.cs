using Microsoft.AspNetCore.Diagnostics;

namespace Carbon.Core.Middlewares;

public class ExceptionHandlerMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}