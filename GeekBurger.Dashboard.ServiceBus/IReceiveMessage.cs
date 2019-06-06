using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public interface IReceiveMessage
    {
        Task ReceiveMessage(string topicName, string subscriptionName);
        Task MessageHandler(Message message, CancellationToken cancellationToken);
        Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs);
    }
}
