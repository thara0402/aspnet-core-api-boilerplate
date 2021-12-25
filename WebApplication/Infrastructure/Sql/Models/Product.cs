using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Infrastructure.Sql.Models
{
    public partial class Product
    {
        public Product()
        {
            BasketItems = new HashSet<BasketItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public long ProductId { get; set; }
        public string Desc { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
