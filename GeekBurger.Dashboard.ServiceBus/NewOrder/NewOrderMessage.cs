using GeekBurger.Dashboard.Repository.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus.NewOrder
{
    public class NewOrderMessage
    {
        private readonly ServiceBusInfo _serviceBusInfo;
        private readonly ISalesRepository _salesRepository;
        private ISubscriptionClient _subscriptionClient;

        public NewOrderMessage(ISalesRepository salesRepository)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile("appsettings.json")
                                                                     .Build();

            _serviceBusInfo = configuration.GetSection("ServiceBus").Get<ServiceBusInfo>();
            _salesRepository = salesRepository;
        }

        public async Task ReceiveMessage(string topicName, string subscriptionName)
        {
            _subscriptionClient = new SubscriptionClient(_serviceBusInfo.ConnectionString, topicName, subscriptionName);

            // MessageHandler para tratamento de exceções, número de mensagens simultâneas a serem processadas
            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler)
            {
                // Número máximo de chamadas simultâneas para o retorno da chamada MessageHandler
                // Configurar de acordo com quantas mensagens o aplicativo vai processar em paralelo.
                MaxConcurrentCalls = 1,

                // False indica que o "Complete" será tratado pelo retorno da chamada em MessageHandler
                AutoComplete = false
            };

            // Registra o método que processará as mensagens
            _subscriptionClient.RegisterMessageHandler(MessageHandler, messageHandlerOptions);

            await _subscriptionClient.CloseAsync();
        }

        public async Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            NewOrder newOrder = JsonConvert.DeserializeObject<NewOrder>(Encoding.UTF8.GetString(message.Body));

            // Verificar de qual pedido pertence essa alteração e fazer o updade
            // Sales sales;
            // _salesRepository.Update(sales);

            // "Finaliza" a mensagem para que ela não seja recebida novamente
            // Isso pode ser feito se o _subscriptionClient for criado no modo ReceiveMode.PeekLock(que é o padrão)
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        public Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");

            ExceptionReceivedContext exceptionReceivedContext = exceptionReceivedEventArgs.ExceptionReceivedContext;

            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {exceptionReceivedContext.Endpoint}");
            Console.WriteLine($"- Entity Path: {exceptionReceivedContext.EntityPath}");
            Console.WriteLine($"- Executing Action: {exceptionReceivedContext.Action}");

            return Task.CompletedTask;
        }
    }
}
