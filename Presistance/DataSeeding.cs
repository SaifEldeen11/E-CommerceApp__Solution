using Domain.Models.ProductModule;
using Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;
using Presistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {

        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                #region Brands
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandsData = File.OpenRead(@"..\Presistance\\Data\\DataSeed\\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }
                #endregion

                #region Types
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead(@"C:\Users\merom\Desktop\RouteBackEnd\API\E-CommerceApp__Solution\Presistance\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductTypes);
                    }
                }
                #endregion

                #region Products
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"C:\Users\merom\Desktop\RouteBackEnd\API\E-CommerceApp__Solution\Presistance\Data\DataSeed\products.json");
                    var Product = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductData);
                    if (Product is not null && Product.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(Product);
                    }
                }
                #endregion

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // to do 
            }

        }
    }
}
