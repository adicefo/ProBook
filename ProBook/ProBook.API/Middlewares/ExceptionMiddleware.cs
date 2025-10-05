using ProBook.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProBook.API.Middleware
{
    public class ExceptionMiddleware
    {
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
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = ex switch
            {
                ValidationException => HttpStatusCode.BadRequest,          // 400
                NotFoundException => HttpStatusCode.NotFound,              // 404
                DuplicateException => HttpStatusCode.Conflict,             // 409
                ForbiddenException => HttpStatusCode.Forbidden,            // 403
                UnauthorizedException => HttpStatusCode.Unauthorized,      // 401
                _ => HttpStatusCode.InternalServerError                    //500
            };

            var result = JsonSerializer.Serialize(new
            {
                statusCode = (int)statusCode,
                errorMessage = ex.Message,
                exceptionType = ex.GetType().Name
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
