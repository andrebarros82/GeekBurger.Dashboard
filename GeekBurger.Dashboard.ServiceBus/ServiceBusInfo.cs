using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.ServiceBus
{
    public class ServiceBusInfo
    {
        public string ResourceGroup { get; }
        public string NamespaceName { get; }
        public string ConnectionString { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string SubscriptionId { get; }
        public string TenantId { get; }
    }
}
