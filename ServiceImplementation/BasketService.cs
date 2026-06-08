using AutoMapper;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Domain.RepoInterfaces;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketModel = _mapper.Map<BasketDto,Basket>(basket);

            var CreateOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basketModel);

            if (CreateOrUpdatedBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }

            throw new Exception("Can't Update or Create Basket now , Try Again Later ");
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
             return await _basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
           var basketToReturn = await _basketRepository.GetBasketAsync(key);

            if(basketToReturn == null)
            {
                throw new BasketNotFoundException(key);
            }
            return _mapper.Map<Basket,BasketDto>(basketToReturn);
        }
    }
}
