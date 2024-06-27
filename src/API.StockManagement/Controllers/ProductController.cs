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
    [Route("api/product")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpPost()]
        [Authorize(Roles = "StockManager, Admin")]
        [ProducesResponseType(typeof(CreateProductResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Product.CreateProduct)]
        public async Task<IResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _productService.CreateProduct(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet()]
        [Authorize(Roles = "StockManager, Admin")]
        [ProducesResponseType(typeof(GetProductsResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Product.GetProducts)]
        public async Task<IResult> GetProducts()
        {
            var result = await _productService.GetProducts();
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("{ProductId}")]
        [Authorize(Roles = "StockManager, Admin")]
        [ProducesResponseType(typeof(GetProductByIdResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Product.GetProductById)]
        public async Task<IResult> GetProductById([FromRoute] GetProductByIdRequest request)
        {
            var result = await _productService.GetProductById(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPut()]
        [Authorize(Roles = "StockManager, Admin")]
        [ProducesResponseType(typeof(UpdateProductResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Product.UpdateProduct)]
        public async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            var result = await _productService.UpdateProduct(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpDelete()]
        [Authorize(Roles = "StockManager, Admin")]
        [ProducesResponseType(typeof(DeleteProductResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Product.DeleteProduct)]
        public async Task<IResult> DeleteProduct([FromBody] DeleteProductRequest request)
        {
            var result = await _productService.DeleteProduct(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("offer-by-budget/{Amount}")]
        [Authorize]
        [ProducesResponseType(typeof(ProductOfferByBudgetResponse), 200)]
        [SwaggerOperation(Summary = Documentation.Product.ProductOffetByBudget)]
        public async Task<IResult> ProductOfferByBudget([FromRoute] ProductOfferByBudgetRequest request)
        {
            var result = await _productService.ProductOfferByBudget(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }
    }
}
