﻿using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Orderitem
    {
        public Orderitem()
        {
            Exchanges = new HashSet<Exchange>();
        }

        public long Id { get; set; }
        public long Quantity { get; set; }
        public long OrderId { get; set; }
        public long ProdcutId { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Prodcut { get; set; } = null!;
        public virtual ICollection<Exchange> Exchanges { get; set; }
    }
}
