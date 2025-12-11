using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ProductModule
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }
        public int BradnId { get; set; } // FK
        public int TypeId { get; set; } // FK
        public ProductBrand ProductBrand { get; set; } = null!;

        public ProductType ProductType { get; set; } = null!;
    }
}
