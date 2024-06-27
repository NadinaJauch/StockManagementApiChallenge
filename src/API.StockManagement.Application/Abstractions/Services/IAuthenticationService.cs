using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.Domain.Common;

namespace API.StockManagement.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        public Task<Result<AuthResponse>> Authenticate(AuthRequest request);
    }
}
