using Shared.DataTransferObjects.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        // Create Order 
        // Take : Order Dto
        // Return : Order To Return Dto
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);

        // Get Delivery Methods 
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        // Get All Orders 
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        // Get Order By Id
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
