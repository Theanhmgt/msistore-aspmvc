﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public Guid? BrandId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
