using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using ServiceImplementation.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            // Mapper Registration
            Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(AssemblyRefrence).Assembly);

            Services.AddScoped<IServiceManger, ServiceManger>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<ICacheService, CacheService>();

            Services.AddScoped<Func<IProductService>>(provider =>
            {
                return () => provider.GetRequiredService<IProductService>();
            });

            Services.AddScoped<Func<IBasketService>>(provider =>
            {
                return () => provider.GetRequiredService<IBasketService>();
            });

            Services.AddScoped<Func<IAuthenticationService>>(provider =>
            {
                return () => provider.GetRequiredService<IAuthenticationService>();
            });

            Services.AddScoped<Func<IOrderService>>(provider =>
            {
                return () => provider.GetRequiredService<IOrderService>();
            });

            return Services;
        }
    }
}
