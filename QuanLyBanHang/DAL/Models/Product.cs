﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            Orderitems = new HashSet<Orderitem>();
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public long? BrandId { get; set; }
        public long CategoryId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Orderitem> Orderitems { get; set; }
    }
}
