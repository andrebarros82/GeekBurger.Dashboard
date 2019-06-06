using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus.OrderChanged
{
    public interface IOrderChangedMessage : IReceiveMessage
    { 

    }
}
