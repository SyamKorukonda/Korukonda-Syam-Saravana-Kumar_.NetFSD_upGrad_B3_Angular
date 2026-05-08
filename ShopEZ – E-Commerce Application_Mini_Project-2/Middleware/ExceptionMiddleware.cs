using System.Net;
using System.Text.Json;
using WebApplication17.ApiResponse;
using WebApplication17.Models;

namespace WebApplication17.Middleware
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
                _logger.LogError(ex, "Unhandled Exception occurred");

                context.Response.ContentType = "application/json";

                // Map exception types to HTTP status codes
                var statusCode = ex switch
                {
                    ArgumentException => (int)HttpStatusCode.BadRequest,       // 400
                    InvalidOperationException => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,      // 404
                    _ => (int)HttpStatusCode.InternalServerError              // 500
                };

                // Set HTTP status code in response
                context.Response.StatusCode = statusCode;

                // Create standardized API error response
                var response = new ApiResponse<string>(ex.Message);

                // Convert response object to JSON string
                var json = JsonSerializer.Serialize(response);
                //Write JSON response to client
                await context.Response.WriteAsync(json);
            }
        }
    }
}
