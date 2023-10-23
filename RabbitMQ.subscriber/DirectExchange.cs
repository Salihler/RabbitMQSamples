using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.subscriber
{
    internal class DirectExchange
    {
        public void DirectConsume()
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

            var queueName = "direct-queue-Critical";

            channel.BasicConsume(queueName, false, consumer);

            Console.WriteLine("Logları dinleniyor...");

            consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                channel.BasicAck(e.DeliveryTag,false);
                Console.WriteLine("Gelen Mesaj:" + message);
            };
            Console.WriteLine("Bitti");
            Console.ReadKey();

        }
    }
}




