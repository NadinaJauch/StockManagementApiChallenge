using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.EndpointDocumentations;
using API.StockManagement.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.StockManagement.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        [SwaggerOperation(Summary = Documentation.User.CreateUser)]
        public async Task<IResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _userService.CreateUser(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }
    }
}
