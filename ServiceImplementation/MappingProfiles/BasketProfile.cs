using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();


        }
    }
}
