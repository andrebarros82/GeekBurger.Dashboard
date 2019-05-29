using GeekBurger.Dashboard.Repository.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public class ServiceBusMessage : IServiceBusMessage
    {
        private readonly ServiceBusInfo _serviceBusInfo;
        private readonly ISalesRepository _salesRepository;
        private readonly string _topic;
        private readonly string _subscription;

        public ServiceBusMessage(ISalesRepository salesRepository, string topic, string subscription)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile("appsettings.json")
                                                                     .Build();

            _serviceBusInfo = configuration.GetSection("ServiceBus").Get<ServiceBusInfo>();
            _salesRepository = salesRepository;
            _topic = topic;
            _subscription = subscription;
        }

        public void ReceiveMessage()
        {
            var queueClient = new QueueClient(_serviceBusInfo.ConnectionString, "OrderChanged", ReceiveMode.PeekLock);
            var handlerOptions = new MessageHandlerOptions(ExceptionHandler) { AutoComplete = false, MaxConcurrentCalls = 3 };
            queueClient.RegisterMessageHandler(MessageHandler, handlerOptions);
        }    

        public Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
