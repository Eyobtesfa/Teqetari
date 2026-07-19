using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string correlationalId = System.Guid.NewGuid().ToString("N")[..8];
        context.Request.Headers["X-Correlation-ID"] = correlationalId;
        _logger.LogInformation("Incoming request: {method} {url} (Correlation ID: {correlationalId})", context.Request.Method, context.Request.Path, correlationalId);


        var stopwatch = Stopwatch.StartNew();
        // Call the next middleware in the pipeline

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
        }


        _logger.LogInformation("Outgoing response: {statusCode} in {elapsedMilliseconds} ms (Correlation ID: {correlationalId})", context.Response.StatusCode,
        stopwatch.ElapsedMilliseconds,
        correlationalId);
    }
}