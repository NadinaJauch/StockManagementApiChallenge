using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services;
using API.StockManagement.Application.Services.DTOs.Request;
using API.StockManagement.Domain.Entities;
using API.StockManagement.Domain.Errors;
using API.StockManagement.Infrastructure.Abstractions.Repositories;
using Moq;

namespace API.StockManagement.Tests
{
    public class ProductTesting
    {
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        public ProductTesting()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task ProductOfferByBudget_ShouldReturnBestOffer_WhenProductsAvailable()
        {
            var products = new List<Product>
            {
                new() { Id = 1, Category = "Deportes", Price = 10, LoadDate = DateTime.Now, UpdateDate = DateTime.Now, Description = "Botines" },
                new() { Id = 2, Category = "Deportes", Price = 5, LoadDate = DateTime.Now, UpdateDate = DateTime.Now, Description = "Pelota" },
                new() { Id = 3, Category = "Deportes", Price = 15, LoadDate = DateTime.Now, UpdateDate = DateTime.Now, Description = "Patines" },
                new() { Id = 4, Category = "Arte", Price = 60, LoadDate = DateTime.Now, UpdateDate = DateTime.Now, Description = "Oleos" },
                new() { Id = 5, Category = "Arte", Price = 5, LoadDate = DateTime.Now, UpdateDate = DateTime.Now, Description = "Lienzo" }
            };
            _productRepositoryMock.Setup(repo => repo.GetProductsGroupedByCategory())
                .ReturnsAsync(products.GroupBy(p => p.Category).ToList());

            var request = new ProductOfferByBudgetRequest { Amount = 70 };
            var result = await _productService.ProductOfferByBudget(request);

            Assert.True(result.Value.OfferedPrice == 70 && result.Value.OfferedProducts.Contains(products[0]) && result.Value.OfferedProducts.Contains(products[0]));
        }

        [Fact]
        public async Task ProductOfferByBudget_ShouldReturnNoOffer_WhenNoProductsInCategory()
        {
            var products = new List<Product>
            {
                new() { Id = 1, Category = "Deportes", Price = 10 },
                new() { Id = 2, Category = "Arte", Price = 60 }
            };
            _productRepositoryMock.Setup(repo => repo.GetProductsGroupedByCategory())
                .ReturnsAsync(products.GroupBy(p => p.Category).ToList());

            var request = new ProductOfferByBudgetRequest { Amount = 20 };
            var result = await _productService.ProductOfferByBudget(request);

            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NoOfferFound, result.Error);
        }

        [Fact]
        public async Task ProductOfferByBudget_ShouldReturnNoOffer_WhenNoProductsAtAll()
        {
            // Sin productos en ninguna categoría
            var products = new List<Product>();
            _productRepositoryMock.Setup(repo => repo.GetProductsGroupedByCategory())
                .ReturnsAsync(products.GroupBy(p => p.Category).ToList());

            var request = new ProductOfferByBudgetRequest { Amount = 100000 };
            var result = await _productService.ProductOfferByBudget(request);

            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NoOfferFound, result.Error);
        }

        [Fact]
        public async Task ProductOfferByBudget_ShouldReturnBestOffer_WhenMultipleCategoriesAndNoDuplicates()
        {
            var products = new List<Product>
            {
                new() { Id = 1, Category = "Deportes", Price = 100000 },
                new() { Id = 2, Category = "Deportes", Price = 50000 },
                new() { Id = 3, Category = "Arte", Price = 600000 },
                new() { Id = 4, Category = "Arte", Price = 250000 }
            };
            _productRepositoryMock.Setup(repo => repo.GetProductsGroupedByCategory())
                .ReturnsAsync(products.GroupBy(p => p.Category).ToList());

            var request = new ProductOfferByBudgetRequest { Amount = 300000 };
            var result = await _productService.ProductOfferByBudget(request);

            Assert.True(result.IsSuccess);
            Assert.Equal(300000, result.Value.OfferedPrice);
            var categories = result.Value.OfferedProducts.Select(p => p.Category).Distinct().ToList();
            Assert.Equal(2, categories.Count); // Deberían ser dos categorías diferentes
        }

        [Fact]
        public async Task ProductOfferByBudget_ShouldReturnNoOffer_WhenBudgetIsTooLow()
        {
            var products = new List<Product>
            {
                new() { Id = 1, Category = "Deportes", Price = 100000 },
                new() { Id = 2, Category = "Arte", Price = 600000 }
            };
            _productRepositoryMock.Setup(repo => repo.GetProductsGroupedByCategory())
                .ReturnsAsync(products.GroupBy(p => p.Category).ToList());

            var request = new ProductOfferByBudgetRequest { Amount = 50000 }; // Presupuesto demasiado bajo para cualquier producto
            var result = await _productService.ProductOfferByBudget(request);

            // Verificar que se devuelve un error adecuado cuando no hay oferta disponible
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NoOfferFound, result.Error);
        }
    }
}