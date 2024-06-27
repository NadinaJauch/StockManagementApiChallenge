using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Application.Services.DTOs.Response;
using API.StockManagement.Domain.Common;
using API.StockManagement.Domain.Entities;
using API.StockManagement.Domain.Errors;
using API.StockManagement.Infrastructure.Abstractions.Repositories;

namespace API.StockManagement.Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Result<CreateProductResponse>> CreateProduct(CreateProductRequest request)
        {
            Product newProduct = new()
            {
                Price = request.Price,
                LoadDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Category = request.Category,
                Description = request.Description,
            };
            await _productRepository.AddAsync(newProduct);
            return new CreateProductResponse() { ProductId = newProduct.Id };
        }

        public async Task<Result<GetProductsResponse>> GetProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            if (products == null || !products.Any())
                return Result.Failure<GetProductsResponse>(ProductErrors.NoProductsAvailable);
            return new GetProductsResponse() { Products = products };
        }

        public async Task<Result<GetProductByIdResponse>> GetProductById(GetProductByIdRequest request)
        {
            Product? product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return Result.Failure<GetProductByIdResponse>(ProductErrors.NotFound);
            return Result.Success(new GetProductByIdResponse() { Product = product });
        }

        public async Task<Result<UpdateProductResponse>> UpdateProduct(UpdateProductRequest request)
        {
            Product? product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return Result.Failure<UpdateProductResponse>(ProductErrors.NotFound);

            product.Price = request.Price;
            product.UpdateDate = DateTime.Now;
            product.Category = request.Category;
            product.Description = request.Description;

            bool result = await _productRepository.UpdateAsync(product);
            return new UpdateProductResponse() { Success =  result };
        }

        public async Task<Result<DeleteProductResponse>> DeleteProduct(DeleteProductRequest request)
        {
            if (!await _productRepository.AnyByIdAsync(request.ProductId))
                return Result.Failure<DeleteProductResponse>(ProductErrors.NotFound);

            bool succees = await _productRepository.DeleteById(request.ProductId);
            return new DeleteProductResponse() { Success = succees };
        }

        public async Task<Result<ProductOfferByBudgetResponse>> ProductOfferByBudget(ProductOfferByBudgetRequest request)
        {
            List<IGrouping<string, Product>> productsGroupedByCategory = await _productRepository.GetProductsGroupedByCategory();

            decimal maxOfferedPrice = 0;
            List<Product> bestOfferedProducts = null;

            foreach (var category1 in productsGroupedByCategory)
            {
                var productsInCategory1 = category1.ToList();

                foreach (var category2 in productsGroupedByCategory.Where(c => c.Key != category1.Key))
                {
                    var productsInCategory2 = category2.ToList();
                    var pairWithMaxPrice = productsInCategory1
                        .SelectMany(p1 => productsInCategory2.Select(p2 => new
                        {
                            Product1 = p1,
                            Product2 = p2,
                            TotalPrice = p1.Price + p2.Price
                        }))
                        .Where(pair => pair.TotalPrice <= request.Amount)
                        .OrderByDescending(pair => pair.TotalPrice)
                        .FirstOrDefault();

                    if (pairWithMaxPrice != null && pairWithMaxPrice.TotalPrice > maxOfferedPrice)
                    {
                        maxOfferedPrice = pairWithMaxPrice.TotalPrice;
                        bestOfferedProducts = [pairWithMaxPrice.Product1, pairWithMaxPrice.Product2];
                    }
                }
            }

            if (maxOfferedPrice == 0)
                return Result.Failure<ProductOfferByBudgetResponse>(ProductErrors.NoOfferFound);

            return new ProductOfferByBudgetResponse
            {
                OfferedPrice = maxOfferedPrice,
                OfferedProducts = bestOfferedProducts
            };
        }
    }
}
