using API.StockManagement.Domain.Common;

namespace API.StockManagement.Domain.Errors
{
    public class ProductErrors
    {
        public static Error NotFound => Error.NotFound(
            "Product.NotFound", $"Product not found");
        public static Error NoProductsAvailable => Error.NoContent(
            "Product.NotFound", $"No products available for display");
        public static Error NoOfferFound => Error.NotFound(
            "Product.NoOfferFound", $"Not product offer available for the indicated budget");
    }
}
