using AutoMapper;
using Domain.Exceptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Domain.RepoInterfaces;
using ServiceAbstraction;
using ServiceImplementation.Specification;
using Shared.DataTransferObjects.IdentityModule;
using Shared.DataTransferObjects.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // Map Address Dto to Order Address
            var address = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            // Get Basket
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);

            if(basket == null)
            {
                throw new BasketNotFoundException(orderDto.BasketId);
            }

            var existingOrderSpecs = new OrderWithPayemtnIntentSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.GetRepository<Order, Guid>()
                               .GetByIdAsync(existingOrderSpecs);

            if(existingOrder is not null)
            {
                _unitOfWork.GetRepository<Order, Guid>().Remove(existingOrder);
            }

            List<OrderItem> orderItems = new List<OrderItem>();
            var PorductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await PorductRepo.GetByIdAsync(item.Id);

                if(product == null)
                {
                    throw new ProductNotFoundException(item.Id);
                }

                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        PictureUrl = product.PictureUrl,
                        ProductName = product.Name,
                    },
                    Price  = product.Price,
                    Quantity = item.Quantity,

                };
                orderItems.Add(OrderItem);
            }

            // Get Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);

            if(deliveryMethod is null)
            {
                throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            }

            // Calculate Sub Total 
            var SubTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            var order = new Order(email, address, deliveryMethod, orderItems, SubTotal,basket.PaymentIntentId);

            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);

            await _unitOfWork.SvaeChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var spec = new OrderSpecifications(email);
            var orders = await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecifications(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);

            if(order is null)
            {
                throw new OrderNotFoundException(id);
            }
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }
    }
}
