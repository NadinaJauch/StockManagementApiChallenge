using API.StockManagement.Infrastructure.Abstractions.Identity;
using API.StockManagement.Infrastructure.Abstractions.Repositories;
using API.StockManagement.Infrastructure.Security.Services;
using API.StockManagement.Infrastructure.Persistence;
using API.StockManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.StockManagement.Infrastructure.Abstractions;
using API.StockManagement.Infrastructure.Security;

namespace API.StockManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("API.StockManagement.Infrastructure")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

            return services;
        }
    }
}
