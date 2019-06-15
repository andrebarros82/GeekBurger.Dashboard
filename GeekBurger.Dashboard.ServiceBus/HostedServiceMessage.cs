using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public class HostedServiceMessage : IHostedService
    {
        private readonly ILogger _logger;       
        private readonly ISalesRepository _salesRepository;
        private ISubscriptionClient _subscriptionClientOrderChanged;
        private ISubscriptionClient _subscriptionClientNewOrder;

        public HostedServiceMessage(ISalesRepository salesRepository, IConfiguration configuration, ILogger<HostedServiceMessage> logger)
        {
            ServiceBusInfo serviceBusInfo = configuration.GetSection("ServiceBus").Get<ServiceBusInfo>();

            _salesRepository = salesRepository;      

            _logger = logger;

            _subscriptionClientOrderChanged = new SubscriptionClient(serviceBusInfo.ConnectionString, "orderchanged", "Dashboard");
            _subscriptionClientNewOrder = new SubscriptionClient(serviceBusInfo.ConnectionString, "neworder", "Dashboard");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {         
            DoWork();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        private void DoWork()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            // Receber mensagens em um loop
            ReceiveMessage();

            Console.ReadKey();

            await _subscriptionClientOrderChanged.CloseAsync();
            await _subscriptionClientNewOrder.CloseAsync();
        }

        private void ReceiveMessage()
        { 
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
            _subscriptionClientOrderChanged.RegisterMessageHandler(MessageHandlerOrderChanged, messageHandlerOptions);
            _subscriptionClientNewOrder.RegisterMessageHandler(MessageHandlerNewOrder, messageHandlerOptions);
        }

        public async Task MessageHandlerOrderChanged(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Mensagem recebida: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            Sales sales = JsonConvert.DeserializeObject<Sales>(Encoding.UTF8.GetString(message.Body));
            State state = sales.State;

            sales = _salesRepository.GetByOrderId(sales.OrderId);
            sales.State = state;

            _salesRepository.Update(sales);

            // "Finaliza" a mensagem para que ela não seja recebida novamente
            // Isso pode ser feito se o subscriptionClient for criado no modo ReceiveMode.PeekLock(que é o padrão)
            await _subscriptionClientOrderChanged.CompleteAsync(message.SystemProperties.LockToken);
        }

        public async Task MessageHandlerNewOrder(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Mensagem recebida: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            Sales sales = JsonConvert.DeserializeObject<Sales>(Encoding.UTF8.GetString(message.Body));

            if (!_salesRepository.OrderExists(sales.OrderId))
            {
                sales.State = State.Open;
                _salesRepository.Insert(sales);
            }

            // "Finaliza" a mensagem para que ela não seja recebida novamente
            // Isso pode ser feito se o subscriptionClient for criado no modo ReceiveMode.PeekLock(que é o padrão)
            await _subscriptionClientNewOrder.CompleteAsync(message.SystemProperties.LockToken);
        }

        public Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogInformation($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");

             ExceptionReceivedContext exceptionReceivedContext = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogInformation("Exception context for troubleshooting:");
            _logger.LogInformation($"- Endpoint: {exceptionReceivedContext.Endpoint}");
            _logger.LogInformation($"- Entity Path: {exceptionReceivedContext.EntityPath}");
            _logger.LogInformation($"- Executing Action: {exceptionReceivedContext.Action}");

            return Task.CompletedTask;
        }
    }
}
