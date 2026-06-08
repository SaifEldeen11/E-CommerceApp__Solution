using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal,string paymentIntentId)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress Address { get; set; } =default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; } // Fk
        public string PaymentIntentId { get; set; } = default!;
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal Subtotal { get; set; }
        public decimal GetTotal()
        {
            return Subtotal+DeliveryMethod.Price;
        }
    }
}
