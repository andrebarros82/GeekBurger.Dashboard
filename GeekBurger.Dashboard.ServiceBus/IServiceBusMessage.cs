using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public interface IServiceBusMessage
    {
        void ReceiveMessage();
        Task MessageHandler(Message message, CancellationToken cancellationToken);

        Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs);
    }
}
