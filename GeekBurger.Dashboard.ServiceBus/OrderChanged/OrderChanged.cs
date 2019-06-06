using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.ServiceBus.OrderChanged
{
    public class OrderChanged
    {
        public string OrderId { get; set; }
        public string StoreId { get; set; }
        public string State { get; set; }
    }
}
