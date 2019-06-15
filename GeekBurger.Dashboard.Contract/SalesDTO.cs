using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Contract
{
    public class SalesDTO
    {
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string Total { get; set; }
        public string Value { get; set; }
    }
}
