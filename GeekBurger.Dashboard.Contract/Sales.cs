﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Contract
{
    public class Sales
    {
        public Guid StoredId { get; set; }
        public int Total { get; set; }
        public string Value { get; set; }
    }
}
