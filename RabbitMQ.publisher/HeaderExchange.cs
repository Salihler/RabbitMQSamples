using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.publisher
{
    internal class HeaderExchange
    {
        public void HeaderPublish()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers);

            Dictionary<string, object> headers = new()
            {
                { "format", "pdf" },
                { "shape", "a4" }
            };

            var props = channel.CreateBasicProperties();

            props.Headers = headers;

                channel.BasicPublish("header-exchange", string.Empty, props, Encoding.UTF8.GetBytes("Header mesajı"));
                Console.WriteLine("Mesaj gönderildi : ");
        }
    }
}
