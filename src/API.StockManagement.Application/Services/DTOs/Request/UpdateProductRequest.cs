using System.ComponentModel.DataAnnotations;

namespace API.StockManagement.Application.Services.DTOs.Request
{
    public class UpdateProductRequest
    {
        public required int ProductId { get; set; }
        [Range(0, 999999999999999999.99)]
        public required decimal Price { get; set; }
        public required string Category { get; set; }
        public string Description { get; set; }
    }
}
