﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Image
    {
        public Guid Id { get; set; }
        public string File { get; set; } = null!;
        public short Preview { get; set; }
        public Guid ProductId { get; set; }
    }
}
