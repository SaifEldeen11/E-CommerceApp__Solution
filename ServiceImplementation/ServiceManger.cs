using AutoMapper;
using Domain.Models.IdentityModule;
using Domain.RepoInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManger(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository,UserManager<ApplicationUser> _userManager,IConfiguration _configuration):IServiceManger
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,_configuration,_mapper));
        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepository, _unitOfWork));
        private readonly Lazy<IPaymentService> _LazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_configuration, _basketRepository, _unitOfWork, _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        public IOrderService OrderService => _LazyOrderService.Value;

        public IPaymentService PaymentService => _LazyPaymentService.Value;
    }
}
