using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using API.StockManagement.Domain.Entities;

namespace API.StockManagement.Infrastructure.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1, RoleDescription = "Admin" },
                new Role { Id = 2, RoleDescription = "StockManager" },
                new Role { Id = 3, RoleDescription = "Customer" }
            );
        }
    }
}
