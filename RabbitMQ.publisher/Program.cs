using RabbitMQ.Client;
using RabbitMQ.publisher;
using System.Text;

TopicExchange topicExchange = new TopicExchange();
DirectExchange directExchange = new DirectExchange();
HeaderExchange headerExchange = new HeaderExchange();

headerExchange.HeaderPublish();

