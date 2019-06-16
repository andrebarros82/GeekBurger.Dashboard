using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Contract
{
    public class SalesDTO
    {
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public int Total { get; set; }
        public decimal Value { get; set; }
    }
}
