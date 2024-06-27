using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.EndpointDocumentations;
using API.StockManagement.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.StockManagement.Controllers
{
    [Route("api/auth")]
    public class AuthController(IAuthenticationService authService) : ControllerBase
    {
        private readonly IAuthenticationService _authService = authService;

        [HttpPost()]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Auth.Authenticate)]
        public async Task<IResult> Authenticate([FromBody] AuthRequest request)
        {
            var result = await _authService.Authenticate(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }
    }
}
