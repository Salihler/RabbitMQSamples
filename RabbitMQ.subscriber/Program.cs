using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.subscriber;
using System.Text;

DirectExchange topictExchange = new DirectExchange();

HeaderExchange headerExchange = new HeaderExchange();


headerExchange.HeaderConsume();
    
