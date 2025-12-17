using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModule
{
    public class Basket
    {
        public string Id { get; set; } // GUID , Created from the client [Front-End]

        public List<BasketItem> Items { get; set; } = [];
    }
}
