using GeekBurger.Dashboard.ServiceBus.OrderChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public class ServiceBusReceiveMessages : IServiceBusReceiveMessages
    {
        private readonly IOrderChangedMessage _orderChangedMessage;

        public ServiceBusReceiveMessages(IOrderChangedMessage orderChangedMessage)
        {
            _orderChangedMessage = orderChangedMessage;

            ReceiveMessages();
        }

        public void ReceiveMessages()
        {
            //ORDER CHANGED
            _orderChangedMessage.ReceiveMessage("OrderChanged", "Production");
            // Outras mensagens
        }
    }
}
