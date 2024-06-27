using API.StockManagement.Domain.Common;
using API.StockManagement.Domain.Enums;
using System.Text.Json;

namespace API.StockManagement.Extensions
{
    public static class ResultExtension
    {
        private const string BadRequest = "Bad Request";
        private const string NotFound = "Not Found";
        private const string Conflict = "Conflict";
        private const string ServerFailure = "Internal Server Error";
        private const string Unauthorized = "Unauthorized";
        private const string NoContent = "NoContent";

        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Results.Json(
                data: new
                {
                    StatusCode = GetStatusCode(result.Error.Type),
                    Title = GetTitle(result.Error.Type),
                    ErrorDescription = result.Error.Description,
                    Type = GetType(result.Error.Type)
                },
                options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase },
                contentType: "application/json",
                statusCode: GetStatusCode(result.Error.Type));

            static int GetStatusCode(ErrorTypeEnum errorType) =>
                errorType switch
                {
                    ErrorTypeEnum.Validation => StatusCodes.Status400BadRequest,
                    ErrorTypeEnum.NotFound => StatusCodes.Status404NotFound,
                    ErrorTypeEnum.Conflict => StatusCodes.Status409Conflict,
                    ErrorTypeEnum.Unauthorized => StatusCodes.Status401Unauthorized,
                    ErrorTypeEnum.NoContent => StatusCodes.Status204NoContent,
                    _ => StatusCodes.Status500InternalServerError
                };

            static string GetTitle(ErrorTypeEnum errorType) =>
                errorType switch
                {
                    ErrorTypeEnum.Validation => BadRequest,
                    ErrorTypeEnum.NotFound => NotFound,
                    ErrorTypeEnum.Conflict => Conflict,
                    ErrorTypeEnum.Unauthorized => Unauthorized,
                    ErrorTypeEnum.NoContent => NoContent,
                    _ => ServerFailure
                };

            static string GetType(ErrorTypeEnum statusCode) =>
                statusCode switch
                {
                    ErrorTypeEnum.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    ErrorTypeEnum.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    ErrorTypeEnum.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    ErrorTypeEnum.Unauthorized => "https://tools.ietf.org/html/rfc7231#section-3.1",
                    ErrorTypeEnum.NoContent => "https://tools.ietf.org/html/rfc7231#section-6.3.5",
                    _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };
        }
    }
}
