using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.publisher
{
    internal class TopicExchange
    {
        public void TopicPublish()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://vapyuvbh:D0gvzd8SnBuca-UiKEAMOVus7FHCJp_2@cow.rmq2.cloudamqp.com/vapyuvbh");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic);

            Random random = new Random();

            for (int i = 0; i <= 50; i++)
            {
                LogNames log1 = (LogNames)random.Next(1, 5);
                LogNames log2 = (LogNames)random.Next(1, 5);
                LogNames log3 = (LogNames)random.Next(1, 5);

                var routeKey = $"{log1}.{log2}.{log3}";

                string message = $"Log-Type : {log1}-{log2}-{log3}";

                var messageBody = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish("logs-topic", routeKey, null, messageBody);
                Console.WriteLine("Mesaj gönderildi : " + messageBody);
            }
        }
    }
}
