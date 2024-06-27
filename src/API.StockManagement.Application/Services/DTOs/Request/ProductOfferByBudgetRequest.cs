using System.ComponentModel.DataAnnotations;

namespace API.StockManagement.Application.Services.DTOs.Request
{
    public class ProductOfferByBudgetRequest
    {
        [Range(1, 1000000)]
        public required int Amount { get; set; }
    }
}
