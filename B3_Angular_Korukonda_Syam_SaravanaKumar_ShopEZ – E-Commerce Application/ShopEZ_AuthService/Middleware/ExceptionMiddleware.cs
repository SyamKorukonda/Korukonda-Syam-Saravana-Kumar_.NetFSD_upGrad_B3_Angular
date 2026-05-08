
using System.Net;
using System.Text.Json;
using ShopEZ_AuthService.Common;

namespace ShopEZ_AuthService.Middleware
{
    //Global Exception Handling Middleware
    // Catches all unhandled exceptions in the application
    public class ExceptionMiddleware
    {
        // Delegate to call next middleware in pipeline
        private readonly RequestDelegate _next;

        // Logger to log errors
        //Logger is used to record application events, errors, and information
        private readonly ILogger<ExceptionMiddleware> _logger;

        // Constructor (Dependency Injection)
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in AuthService: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Map exception types to HTTP status codes and set it in response
            context.Response.StatusCode = ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            // Create standardized API error response
            var response = new ApiResponse<string>(ex.Message);

            // Convert response object to JSON string
            var json = JsonSerializer.Serialize(response);

            //Write JSON response to client
            return context.Response.WriteAsync(json);
        }
    }
}
