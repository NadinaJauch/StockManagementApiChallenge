using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.Domain.Common;
using API.StockManagement.Domain.Entities;
using API.StockManagement.Domain.Errors;
using API.StockManagement.Infrastructure.Abstractions.Identity;
using API.StockManagement.Infrastructure.Abstractions.Repositories;

namespace API.StockManagement.Application.Services
{
    public class UserService(IRoleRepository roleRepository,
                             IUserRepository userRepository,
                             ISecurityService securityService) : IUserService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISecurityService _securityService = securityService;

        public async Task<Result<CreateUserResponse>> CreateUser(CreateUserRequest request)
        {
            if (await _userRepository.AnyByUsername(request.Username))
                return Result.Failure<CreateUserResponse>(UserErrors.AlreadyExists(request.Username));

            if (!await _roleRepository.AnyById(request.RoleId))
                return Result.Failure<CreateUserResponse>(RoleErrors.NotFound);

            User newUser = new()
            {
                Username = request.Username,
                Password = _securityService.HashPassword(request.Password),
                RoleId = request.RoleId
            };
            await _userRepository.AddAsync(newUser);

            return Result.Success(new CreateUserResponse() { UserId = newUser.Id });
        }
    }
}
