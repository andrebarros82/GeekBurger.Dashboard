using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using GeekBurger.Dashboard.Services;
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
        private readonly ISalesService _salesService;
        private readonly IUserWithLessOfferService _userWithLessOfferService;
        private readonly ILogMessage _logMessage;
        private ISubscriptionClient _subscriptionClientOrderChanged;
        private ISubscriptionClient _subscriptionClientNewOrder;
        private ISubscriptionClient _subscriptionClientUserWithLessOffer;

        public HostedServiceMessage(ISalesService salesService, IUserWithLessOfferService userWithLessOfferService,
                                    IConfiguration configuration, ILogger<HostedServiceMessage> logger, 
                                    ILogMessage logMessage)
        {
            ServiceBusInfo serviceBusInfo = configuration.GetSection("ServiceBus").Get<ServiceBusInfo>();

            _salesService = salesService;
            _userWithLessOfferService = userWithLessOfferService;
            _logMessage = logMessage;

            _logger = logger;

            _subscriptionClientUserWithLessOffer = new SubscriptionClient(serviceBusInfo.ConnectionString, "userwithlessoffer", "Dashboard");
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
            // Receber mensagens em um loop
            ReceiveMessage();
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
            _subscriptionClientUserWithLessOffer.RegisterMessageHandler(MessageHandlerUserWithLessOffer, messageHandlerOptions);
            _subscriptionClientOrderChanged.RegisterMessageHandler(MessageHandlerOrderChanged, messageHandlerOptions);
            _subscriptionClientNewOrder.RegisterMessageHandler(MessageHandlerNewOrder, messageHandlerOptions);
        }

        private async Task MessageHandlerUserWithLessOffer(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Mensagem recebida: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            UserWithLessOffer userWithLessOffer = JsonConvert.DeserializeObject<UserWithLessOffer>(Encoding.UTF8.GetString(message.Body));

            foreach(string restriction in userWithLessOffer.Restrictions)
            {
                userWithLessOffer.UserRestrictions = new List<UserRestriction>();
                userWithLessOffer.UserRestrictions.Add(new UserRestriction { Restriction = restriction });
            }

            await _userWithLessOfferService.Insert(userWithLessOffer);

            // "Finaliza" a mensagem para que ela não seja recebida novamente
            // Isso pode ser feito se o subscriptionClient for criado no modo ReceiveMode.PeekLock(que é o padrão)
            await _subscriptionClientUserWithLessOffer.CompleteAsync(message.SystemProperties.LockToken);
        }

        public async Task MessageHandlerOrderChanged(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Mensagem recebida: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            Sales sales = JsonConvert.DeserializeObject<Sales>(Encoding.UTF8.GetString(message.Body));
            State state = sales.State;

            sales = await _salesService.GetByOrderId(sales.OrderId);
            sales.State = state;

            await _salesService.Update(sales);

            // "Finaliza" a mensagem para que ela não seja recebida novamente
            // Isso pode ser feito se o subscriptionClient for criado no modo ReceiveMode.PeekLock(que é o padrão)
            await _subscriptionClientOrderChanged.CompleteAsync(message.SystemProperties.LockToken);
        }

        public async Task MessageHandlerNewOrder(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Mensagem recebida: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            Sales sales = JsonConvert.DeserializeObject<Sales>(Encoding.UTF8.GetString(message.Body));

            if (!_salesService.OrderExists(sales.OrderId).Result)
            {     
                await _salesService.Insert(sales);
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

            _logMessage.Log(exceptionReceivedEventArgs.Exception.Message);

            return Task.CompletedTask;
        }
    }
}
