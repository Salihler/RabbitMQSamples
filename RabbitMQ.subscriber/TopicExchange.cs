using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.subscriber
{
    internal class TopicExchange
    {
        public void TopicConsume()
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
            //channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic);

            var queueName = channel.QueueDeclare().QueueName;

            var routeKey = "*.Error.*";

            channel.QueueBind(queueName,exchange :"logs-topic",routeKey);

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




