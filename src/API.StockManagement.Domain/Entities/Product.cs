using System.ComponentModel.DataAnnotations.Schema;

namespace API.StockManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(20, 2)")]
        public required decimal Price { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LoadDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public required string Category { get; set; }
        public string Description { get; set; }
    }
}
