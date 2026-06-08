
using Azure;
using Domain.RepoInterfaces;
using E_CommerceApp.CustomMiddleWares;
using E_CommerceApp.Extenstions;
using E_CommerceApp.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data.Contexts;
using Presistance.Repostires;
using ServiceAbstraction;
using ServiceImplementation;
using ServiceImplementation.MappingProfiles;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace E_CommerceApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            // Service Layer Registration
            builder.Services.AddApplicationServices();

            // Presistance Layer Registration
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddWebApplicationServices();

            builder.Services.AddJWTServices(builder.Configuration);
            #endregion


            var app = builder.Build();

            await app.SeedDataAsync();

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
