using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.Domain.Common;

namespace API.StockManagement.Application.Abstractions.Services
{
    public interface IProductService
    {
        public Task<Result<CreateProductResponse>> CreateProduct(CreateProductRequest request);
        public Task<Result<GetProductsResponse>> GetProducts();
        public Task<Result<GetProductByIdResponse>> GetProductById(GetProductByIdRequest request);
        public Task<Result<UpdateProductResponse>> UpdateProduct(UpdateProductRequest request);
        public Task<Result<DeleteProductResponse>> DeleteProduct(DeleteProductRequest request);
        public Task<Result<ProductOfferByBudgetResponse>> ProductOfferByBudget (ProductOfferByBudgetRequest request);
    }
}
