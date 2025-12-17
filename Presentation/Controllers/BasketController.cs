using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManger _serviceManger) : ControllerBase
    {
        // Get Basket 
        // GET : api/basket/GetBasket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManger.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }
        // Create Or Update Basket
        // POST : api/basket/CreateOrUpdateBasket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var createdOrUpdatedBasket = await _serviceManger.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(createdOrUpdatedBasket);
        }
        // Delete Basket
        // DELETE : api/basket/DeleteBasket/basket01
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
             var result = await _serviceManger.BasketService.DeleteBasketAsync(key);
            return Ok(result);
        }
    }
}
