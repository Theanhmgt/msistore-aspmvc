﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public short IsActive { get; set; }
        public string Uuid { get; set; } = null!;
        public Guid? UserId { get; set; }
    }
}
