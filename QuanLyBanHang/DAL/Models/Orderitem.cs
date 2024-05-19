﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class OrderItem
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; } = null!;
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
