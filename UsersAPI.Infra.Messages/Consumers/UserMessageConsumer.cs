using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Models;
using UsersAPI.Infra.Messages.Services;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Consumers
{
    public class UserMessageConsumer : BackgroundService
    {
        private readonly EmailMessageService? service;
        private readonly IServiceProvider serviceProvider;
        private readonly RabbitMQSettings? settings;
        private IConnection connection;
        private IModel model;
        public UserMessageConsumer(EmailMessageService? service, IServiceProvider serviceProvider,IOptions<RabbitMQSettings>? settings)
        {
            this.service = service;
            this.serviceProvider = serviceProvider;
            this.settings = settings?.Value;

            var factory = new ConnectionFactory { Uri = new Uri(this.settings?.Url) };
            connection = factory.CreateConnection();
            model = connection.CreateModel();

            model.QueueDeclare(
               queue: this.settings?.Queue,
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null
               );
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(model);
            consumer.Received += async (sender, args) =>
            {
                //lendo e deserializando o conteudo da fila
                var payload = Encoding.UTF8.GetString(args.Body.ToArray());
                var userMessageVO = JsonConvert.DeserializeObject<UserMessageVO>(payload);
                //processando o item
                using var scope = serviceProvider.CreateScope();
                var messageRequestModel = new MessageRequestModel
                {
                    MailTo = userMessageVO.To,
                    Subject = userMessageVO.Subject,
                    Body = userMessageVO.Body,
                    IsBodyHtml = true
                };
                //enviando a mensagem para o email do usuario
                await service?.SendMessage(messageRequestModel);
                //removendo item da fila
                model.BasicAck(args.DeliveryTag, false);
            };
            //executando a leitura da fila
            model.BasicConsume(settings?.Queue, false, consumer);
        }
    }

}
