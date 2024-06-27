using API.StockManagement.Domain.Enums;

namespace API.StockManagement.Application.Services.DTOs.Request
{
    public class CreateUserRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }
    }
}
