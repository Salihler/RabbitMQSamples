using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.subscriber
{
    internal class HeaderExchange
    {
        public void HeaderConsume()
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

            var consumer = new EventingBasicConsumer(channel);

            var queueName = channel.QueueDeclare().QueueName;

            Dictionary<string, object> headers = new()
            {
                { "format", "pdf" },
                { "shape", "a4" },
                { "x-match", "all" }
            };

            channel.QueueBind(queueName, exchange: "header-exchange", string.Empty, headers);

            channel.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Logları dinleniyor...");

            consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);

                Console.WriteLine("Gelen Mesaj:" + message);
            };
            Console.WriteLine("bitti");
            Console.ReadKey();
        }
    }
}




