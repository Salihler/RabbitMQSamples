using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Watermarker.Web.Services
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService;

        public RabbitMqPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitmqClientService = rabbitMQClientService;
        }

        public void Publish(ProductImageCreatedEvent productImageCreatedEvent) {
            var channel = _rabbitmqClientService.Connect();

            string bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            byte[] bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(RabbitMQClientService.ExchangeName,RabbitMQClientService.RoutingWatermark,properties,bodyByte);
        }
    }
}
