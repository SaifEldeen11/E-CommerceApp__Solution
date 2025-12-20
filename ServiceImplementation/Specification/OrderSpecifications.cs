using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specification
{
    internal class OrderSpecifications:BaseSpecification<Order,Guid>
    {
        // Get All Orders By email
        public OrderSpecifications(string email):base(O => O.UserEmail == email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
        // Get Order By Id
        public OrderSpecifications(Guid id):base(O => O.Id==id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
