using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Infrastructure.Sql.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public long OrderId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UserId { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
