
using Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data.Contexts;
using Presistance.Repostires;
using ServiceAbstraction;
using ServiceImplementation;
using ServiceImplementation.MappingProfiles;

namespace E_CommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext Registration
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Data Seeding Registration
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            //Unit of Work Registration
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Mapper Registration
            builder.Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(AssemblyRefrence).Assembly);

            // Service Manger Registration
            builder.Services.AddScoped<IServiceManger,ServiceManger>();


            #endregion
            var app = builder.Build();

            var Scope = app.Services.CreateScope();

             var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            seed.DataSeedAsync();

            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}
