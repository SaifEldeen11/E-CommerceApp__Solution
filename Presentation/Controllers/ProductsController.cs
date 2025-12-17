using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // baseUrl/api/Products
    public class ProductsController(IServiceManger _serviceManger): ControllerBase
    {
        // Get All Products
        [HttpGet]
        [Authorize]
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
