using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.publisher
{

    public enum LogNames
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }

    internal class DirectExchange
    {
        public void DirctPublish()
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

            channel.ExchangeDeclare("logs-direct", durable: true, type: ExchangeType.Direct);

            Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
            {
                var routeKey = $"{x}";
                var queueName = $"direct-queue-{x}";
                channel.QueueDeclare(queueName, true, false, false);
                channel.QueueBind(queueName, "logs-direct", routeKey, null);
            });

            for (int i = 0; i <= 50; i++)
            {
                LogNames log = (LogNames)new Random().Next(1, 5);

                string message = $"Log-Type : {log}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                var routeKey = $"{log}";

                channel.BasicPublish("logs-direct", routeKey, null, messageBody);
                Console.WriteLine("Mesaj gönderildi : " + messageBody);
            }
            Console.WriteLine("Bitti");
            Console.ReadKey();

        }
    }
}
