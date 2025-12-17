using AutoMapper;
using Domain.Exceptions;
using Domain.Models.ProductModule;
using Domain.RepoInterfaces;
using ServiceAbstraction;
using ServiceImplementation.Specification;
using Shared;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    internal class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandDtos = _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(brands);
            return brandDtos;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specfication = new ProductWithBrandAndTypeSpecfications(queryParams);
            var products = await repo.GetAllAsync(specfication);
            var ProductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var ProductCount = ProductsDto.Count();
            var CountSpec = new ProductCountSpecfications(queryParams);
            var TotalCount = await repo.CountAsync(CountSpec); 

            return new PaginatedResult<ProductDto>(ProductCount,queryParams.PageIndex ,TotalCount ,ProductsDto);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var types = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specfication = new ProductWithBrandAndTypeSpecfications(id);
            var product =  await repo.GetByIdAsync(specfication);
            if (product == null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<Product, ProductDto>(product);   
        }
    }
}
