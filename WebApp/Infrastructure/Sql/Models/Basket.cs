using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Infrastructure.Sql.Models
{
    public partial class Basket
    {
        public Basket()
        {
            BasketItems = new HashSet<BasketItem>();
        }

        public long BasketId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UserId { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
    }
}
