using System.Text.Json;
using NLog;

namespace API.StockManagement.Middleware
{
    internal sealed class CustomMiddleware : IMiddleware
    {
        private readonly Logger Logger = LogManager.Setup().GetCurrentClassLogger();
        public CustomMiddleware()
        {
        }
        public async Task InvokeAsync(HttpContext context, 
                                      RequestDelegate next)
        {
            Guid guid = Guid.NewGuid();
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, guid);
            }
        }

        private Task HandleException(HttpContext context, 
                                     Exception ex,
                                     Guid guid)
        {
            Logger.Error($"{guid}|{ex.Message}|{ex.InnerException}|{ex.StackTrace}");

            string exceptionResponse = JsonSerializer.Serialize(new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = $"Internal Server Error, GUID: {guid}",
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(exceptionResponse);
        }
    }
}
