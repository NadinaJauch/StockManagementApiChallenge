using API.StockManagement.Domain.Common;

namespace API.StockManagement.Domain.Errors
{
    public static class UserErrors
    {
        public static Error NotFound => Error.NotFound(
            "User.NotFound", $"User not found");
        public static Error AlreadyExists(string username) => Error.Conflict(
            "User.AlreadyExists", $"User with username '{username}' already exists");
    }
}
