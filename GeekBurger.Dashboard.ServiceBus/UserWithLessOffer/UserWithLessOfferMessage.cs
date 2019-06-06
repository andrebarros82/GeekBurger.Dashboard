using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace GeekBurger.Dashboard.ServiceBus.UserWithLessOffer
{
    public class UserWithLessOfferMessage : IUserWithLessOfferMessage
    {
        private readonly ServiceBusInfo _serviceBusInfo;
        private SubscriptionClient _subscriptionClient;

        public UserWithLessOfferMessage()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile("appsettings.json")
                                                                     .Build();

            _serviceBusInfo = configuration.GetSection("ServiceBus").Get<ServiceBusInfo>();  
        }

        public async Task ReceiveMessage(string topicName, string subscriptionName)
        {
            _subscriptionClient = new SubscriptionClient(_serviceBusInfo.ConnectionString, topicName, subscriptionName);
            await _subscriptionClient.CloseAsync();

            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(MessageHandler, messageHandlerOptions);
        }

        public async Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        public Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
