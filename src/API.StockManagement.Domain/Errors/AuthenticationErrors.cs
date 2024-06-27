using API.StockManagement.Domain.Common;

namespace API.StockManagement.Domain.Errors
{
    public static class AuthenticationErrors
    {
        public static Error InvalidCredentials => Error.Unauthorized(
           "Authentication.InvalidCredentials", $"Invalid Credentials");
    }
}
