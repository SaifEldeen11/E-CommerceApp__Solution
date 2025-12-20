using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ProductModule;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Presentation.Controllers
{
    public class ProductsController(IServiceManger _serviceManger): ApiBaseController
    {
        // Get All Products
        [HttpGet]
        [Cache(300)]
        // GET: baseUrl/api/Products

        // Name Asc
        // Name Desc
        // Price Asc
        // Price Desc

        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        { 
            var products =  await _serviceManger.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        // Get Product By Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorToReturn),StatusCodes.Status404NotFound)]
        // GET: baseUrl/api/Products/id
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        // Get All Brands
        [HttpGet("brands")]
        // GET: baseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        // Get All Types
        [HttpGet("types")]
        // GET: baseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManger.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
