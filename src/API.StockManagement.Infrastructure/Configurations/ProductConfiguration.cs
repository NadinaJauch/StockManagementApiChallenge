using API.StockManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace API.StockManagement.Infrastructure.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Price = 80, Category = "category1", Description = "classic", LoadDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Product { Id = 2, Price = 120, Category = "category1", Description = "gold", LoadDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Product { Id = 3, Price = 150, Category = "category1", Description = "black", LoadDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Product { Id = 4, Price = 160, Category = "category2", Description = "classic", LoadDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Product { Id = 5, Price = 230, Category = "category2", Description = "gold", LoadDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Product { Id = 6, Price = 360, Category = "category2", Description = "black", LoadDate = DateTime.Now, UpdateDate = DateTime.Now });
        }
    }
}
