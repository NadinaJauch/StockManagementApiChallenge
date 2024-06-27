using API.StockManagement.Domain.Entities;

namespace API.StockManagement.Application.Services.DTOs.Response
{
    public class GetProductsResponse
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
