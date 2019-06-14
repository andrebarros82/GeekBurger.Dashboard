using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.ServiceBus
{
    public class NewOrder
    {
        public string OrderId { get; set; }

        public string StoreId { get; set; }

        public string Total { get; set; }

        public List<Product> Products { get; set; }

        public string[] ProductionIds { get; set; }

        //OrderId": 1111,   "StoreId": 1111,   "Total": "10.20",   "Products": [{ "ProductId": 1111}]   "ProductionIds": [1111,1112] } 
    }

    public class Product
    {
        public string ProductId { get; set; }
    }
}
