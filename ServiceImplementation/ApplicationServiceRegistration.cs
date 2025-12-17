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

            // Service Manger Registration
            Services.AddScoped<IServiceManger, ServiceManger>();

            return Services;
        }
    }
}
