using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Producers
{
    public class UserMessageProducer : IUserMessageProducer
    {
        private readonly RabbitMQSettings? rabbitMQSettings;

        public UserMessageProducer(IOptions<RabbitMQSettings>? rabbitMQSettings)
        {
            this.rabbitMQSettings = rabbitMQSettings?.Value;
        }

        public void Send(UserMessageVO userMessage)
        {
            var connectionFactory = new ConnectionFactory() { Uri = new Uri(rabbitMQSettings?.Url) };
            using var connection = connectionFactory.CreateConnection();
            using var model = connection.CreateModel();

            model.QueueDeclare(queue: rabbitMQSettings.Queue, durable: true,
                autoDelete: false, exclusive: false, arguments: null);

            model.BasicPublish(exchange: string.Empty, routingKey: rabbitMQSettings.Queue,
                basicProperties: null, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userMessage)));
        }
    }
}
