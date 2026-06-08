using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefualtPageSize = 5;
        private const int MaxPageSize = 10;

        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public ProductSortingOption sortingOption { get; set; }

        public string? SearchValue { get; set; }

        public int PageIndex { get; set; } = 1;

        private int pageSize= DefualtPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set 
            {    
                pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

    }
}
