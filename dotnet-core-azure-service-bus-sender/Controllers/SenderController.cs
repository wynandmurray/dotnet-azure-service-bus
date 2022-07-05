using dotnet_core_azure_service_bus_sender.Models;
using dotnet_core_azure_service_bus_sender.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_azure_service_bus_sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SenderController : ControllerBase
    {
        private readonly ILogger<SenderController> _logger;
        private readonly IMessageSenderService _messageSenderService;        

        public SenderController(ILogger<SenderController> logger, 
                                IMessageSenderService messageSenderService,
                                IConfiguration configuration)
        {
            _logger = logger;
            _messageSenderService = messageSenderService;
        }

        [HttpPost("sendemail")]
        public async Task SendEmail([FromBody] EmailTransportMessage emailTransportMessage)
        {
            _logger.LogInformation($"Sending Message.");

            await _messageSenderService.SendEmail(emailTransportMessage, "emailqueue");

            _logger.LogInformation($"Message sent.");
        }
    }
}