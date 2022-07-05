using System.Text.Json;
using Azure.Messaging.ServiceBus;
using dotnet_core_azure_service_bus_sender.Models;

namespace dotnet_core_azure_service_bus_receiver
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var client = new ServiceBusClient("YOUR_SERVICEBUS_CONNECTION_STRING");
            var processor = client.CreateProcessor("emailqueue", new ServiceBusProcessorOptions { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

            processor.ProcessMessageAsync += HandleMessage;
            processor.ProcessErrorAsync += HandleError;

            await processor.StartProcessingAsync();

            Console.ReadLine();
        }

        private static Task HandleError(ProcessErrorEventArgs arg)
        {
            Console.WriteLine($"Error occurred: {arg.Exception}");

            return Task.CompletedTask;
        }

        private static Task HandleMessage(ProcessMessageEventArgs arg)
        {
            var emailMessageObject = JsonSerializer.Deserialize<EmailTransportMessage>(arg.Message.Body);

            if (emailMessageObject != null)
            {
                Console.WriteLine($"Sending email to: {emailMessageObject.FirstName} {emailMessageObject.LastName} at {emailMessageObject.EmailAddress}");
            }
            else
            {
                throw new InvalidOperationException("Error: Message was null.");
            }

            return Task.CompletedTask;
        }
    }
}