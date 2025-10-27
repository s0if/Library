using System.Net;

namespace Library.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                // ✅ Check status codes after the request runs
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await HandleCustomResponseAsync(
                        context,
                        HttpStatusCode.Unauthorized,
                        "Unauthorized access.",
                        "You are not authorized to access this resource. Please log in or provide a valid token.");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await HandleCustomResponseAsync(
                        context, 
                        HttpStatusCode.Forbidden,
                        "Access is forbidden.",
                        "You do not have permission to perform this action.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt detected.");
                await HandleCustomResponseAsync(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleCustomResponseAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.", ex.Message);
            }
        }

        private static async Task HandleCustomResponseAsync(HttpContext context, HttpStatusCode statusCode, string message, string? details = null)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                message,
                details
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
