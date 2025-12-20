using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrderController(IServiceManger _serviceManger) : ApiBaseController
    {
        // Change the return type of CreateOrder from Task<Action<OrderToReturnDto>> to Task<IActionResult>
        [Authorize]
        [HttpPost] //POST : baseUrl/api/Order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var order = await _serviceManger.OrderService.CreateOrderAsync(orderDto,GetEmailFromToken());
            return Ok(order);
        }

        // Get Delivery Methods
        [Authorize]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManger.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        // Get All Orders By email
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var orders = await _serviceManger.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(orders);
        }

        // Get Order By Id
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManger.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

    }
}
