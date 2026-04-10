namespace WebApplication15.Middlewares
{
    // Developping Custom Middleware
    public class ExceptionMiddleware
    {

 //All this is the default code for every custom exception no need to change it just copy paste and use it 

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error (Message from Custom Exception Middleware)"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }

        // use the below code for custom exception middleware to handle global errors
        // and return standardized responses instead of exposing internal server details.”


        /* public async Task InvokeAsync(HttpContext context)
         {
             try
             {
                 await _next(context);
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, ex.Message);

                 context.Response.ContentType = "application/json";

                 var statusCode = ex switch
                 {
                     KeyNotFoundException => StatusCodes.Status404NotFound,
                     UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                     ArgumentException => StatusCodes.Status400BadRequest,
                     _ => StatusCodes.Status500InternalServerError
                 };

                 context.Response.StatusCode = statusCode;

                 var response = new
                 {
                     StatusCode = statusCode,
                     Message = ex.Message   // show actual error (good for dev)
                 };

                 await context.Response.WriteAsJsonAsync(response);
             }
         }
        */
    }
}
