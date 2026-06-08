using Domain.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specification
{
    internal class ProductWithBrandAndTypeSpecfications:BaseSpecification<Product,int>
    {
        // Get All
        // 1. brandId = null, typeId = null  => all products [true && true]
        // 2. brandId Value, typeId = null  => filter by brandId [true]
        // 3. brandId = null, typeId Value  => filter by typeId [  ]
        // 4. brandId Value, typeId Value  => filter by brandId and typeId

        public ProductWithBrandAndTypeSpecfications(ProductQueryParams queryParams)
            :base(P => ( !queryParams.brandId.HasValue ||P.BrandId==queryParams.brandId) &&
            (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
            
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            #region Sorting
            // Sorting
            switch (queryParams.sortingOption)
            {
                case ProductSortingOption.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOption.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOption.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOption.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;

            } 
            #endregion

            ApplyPagination(queryParams.PageSize,queryParams.PageIndex);


        }

        #region Get By Id
        // Get By Id
        public ProductWithBrandAndTypeSpecfications(int id) : base(p => p.Id == id)
        {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        } 
        #endregion
    }
}
