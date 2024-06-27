using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.Domain.Common;
using API.StockManagement.Infrastructure.Abstractions.Identity;

namespace API.StockManagement.Application.Services
{
    public class AuthenticationService(ISecurityService securityService) : IAuthenticationService
    {
        private readonly ISecurityService _securityService = securityService;

        public async Task<Result<AuthResponse>> Authenticate(AuthRequest request)
        {
            Result<string?> jwtToken = await _securityService.ValidateCredentials(request.Username, request.Password);
            
            if (jwtToken.IsSuccess) 
                return new AuthResponse() { JWTToken = jwtToken.Value };

            return Result.Failure<AuthResponse>(jwtToken.Error);
        }
    }
}
