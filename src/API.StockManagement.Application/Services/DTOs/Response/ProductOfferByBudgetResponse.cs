using API.StockManagement.Domain.Entities;

namespace API.StockManagement.Application.Services.DTOs.Response
{
    public class ProductOfferByBudgetResponse
    {
        public decimal OfferedPrice { get; set; }
        public ICollection<Product> OfferedProducts { get; set; }
    }
}
