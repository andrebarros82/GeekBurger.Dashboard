using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public class LogMessage : ILogMessage
    {
        private readonly string _connectionString;
        private string _message;
        const string TOPIC_NAME = "log";
        static ITopicClient topicClient;

        public LogMessage(IConfiguration configuration)
        {
            ServiceBusInfo serviceBusInfo = configuration.GetSection("ServiceBus").Get<ServiceBusInfo>();

            _connectionString = serviceBusInfo.ConnectionString;
        }

        /// <summary>
        /// Loga exceções ocorridas na API
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            _message = message;
            MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            topicClient = new TopicClient(_connectionString, TOPIC_NAME);

            await SendMessagesAsync();
            await topicClient.CloseAsync();
        }

        /// <summary>
        /// Envia uma messagem para o tópico "log" contendo a exceção.
        /// </summary>
        /// <returns>Task</returns>
        public async Task SendMessagesAsync()
        {
            try
            {
                string messageBody = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")  + " Dashboard: " + _message;
                Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}