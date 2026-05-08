using System.Net;
using System.Text.Json;
using ShopEZ_OrderService.Common;

namespace ShopEZ_OrderService.Middleware
{
    // Global Exception Handling Middleware
    // Catches all unhandled exceptions and returns a standardized ApiResponse
    public class ExceptionMiddleware
    {

        // Delegate to call next middleware in pipeline
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        // Constructor — Dependency Injection
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in OrderService: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Map exception types to HTTP status codes
            context.Response.StatusCode = ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,         // 400
                InvalidOperationException => (int)HttpStatusCode.BadRequest,         // 400
                KeyNotFoundException => (int)HttpStatusCode.NotFound,           // 404
                HttpRequestException => (int)HttpStatusCode.ServiceUnavailable, // 503
                _ => (int)HttpStatusCode.InternalServerError // 500
            };

            // Return standardized error response
            var response = new ApiResponse<string>(ex.Message);
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
