using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specification
{
    internal class OrderWithPayemtnIntentSpecification:BaseSpecification<Order,Guid>
    {
        public OrderWithPayemtnIntentSpecification(string PaymentInentId)
            :base(O => O.PaymentIntentId == PaymentInentId)
        {
            
        }
    }
}
