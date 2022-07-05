using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace dotnet_core_azure_service_bus_sender.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly IConfiguration _configuration;

        public MessageSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail<T>(T message, string queueName)
        {
            if (message != null)
            {
                var client = new ServiceBusClient(_configuration.GetConnectionString("EmailQueueConnectionString"));
                var sender = client.CreateSender("emailqueue");
                var msg = new ServiceBusMessage(JsonSerializer.Serialize(message));

                await sender.SendMessageAsync(msg);
            }
        }
    }
}
