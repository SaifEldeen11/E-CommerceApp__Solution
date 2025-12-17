using Domain.Models.IdentityModule;
using Domain.RepoInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistance.Data.Contexts;
using Presistance.Identity;
using Presistance.Repostires;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public static class InfraStructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<IDataSeeding, DataSeeding>();

            Services.AddScoped<IBasketRepository, BasketRepostiry>();

            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return  ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
            });

            Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();


            return Services;
        }
    }
}
