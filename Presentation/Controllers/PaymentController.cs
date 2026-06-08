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
    public class PaymentController(IServiceManger _serviceManger):ApiBaseController
    {
        [HttpPost("{basketId}")] // POST : baseUrl/api/Payment/{basketId}
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(string basketId)
        {
            var basket = await _serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(basket);
        }

        [HttpPost("WebHook")] //// POST : baseUrl/api/Payment/WebHook
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Response.Body).ReadToEndAsync();
            await _serviceManger.PaymentService.UpdateOrderPaymentStatusAsync(json, Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }

    }
}
