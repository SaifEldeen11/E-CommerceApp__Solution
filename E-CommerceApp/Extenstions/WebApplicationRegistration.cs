using Domain.RepoInterfaces;
using E_CommerceApp.CustomMiddleWares;
using System.Threading.Tasks;

namespace E_CommerceApp.Extenstions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataAsync (this WebApplication app)
        {
            var Scope = app.Services.CreateScope();

            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

           await seed.DataSeedAsync();
           await seed.IdentityDataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
