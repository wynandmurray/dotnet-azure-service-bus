namespace dotnet_core_azure_service_bus_sender.Services
{
    public interface IMessageSenderService
    {
        Task SendEmail<T>(T message, string queueName);
    }
}