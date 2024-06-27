using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.Domain.Common;

namespace API.StockManagement.Application.Abstractions.Services
{
    public interface IUserService
    {
        public Task<Result<CreateUserResponse>> CreateUser(CreateUserRequest request);
    }
}
