using API.StockManagement.Domain.Common;

namespace API.StockManagement.Domain.Errors
{
    public static class RoleErrors
    {
        public static Error NotFound => Error.NotFound(
            "Role.NotFound", $"Role not found");
    }
}
