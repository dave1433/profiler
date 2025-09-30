using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails()
        {
            Title = exception.Message,
        };
        
        httpContext.Response.WriteAsJsonAsync(problemDetails);
        
        return true;
    }
}