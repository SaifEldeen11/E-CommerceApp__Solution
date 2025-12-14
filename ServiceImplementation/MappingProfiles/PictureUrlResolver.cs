using AutoMapper;
using AutoMapper.Execution;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace ServiceImplementation.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }

            return $"{_configration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
