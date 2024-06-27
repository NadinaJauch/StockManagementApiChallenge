using API.StockManagement.Application.Abstractions.Services;
using API.StockManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.StockManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
