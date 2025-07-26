using DesafioBTG.API.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioBTG.API.Messaging
{
    public class PedidoPublisher
    {
        private readonly string _hostName;
        private readonly string _queueName;

        public PedidoPublisher(RabbitMQSettings settings)
        {
            _hostName = settings.HostName
                ?? throw new ArgumentNullException("RabbitMQ", "Parâmetro RabbitMQ não encontrado.");

            _queueName = settings.QueueName
                ?? throw new ArgumentNullException("RabbitMQ", "Parâmetro RabbitMQ não encontrado.");
        }

        public void IncluirPedido<T>(T pedido)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(pedido);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.DeliveryMode = 2; // persistente

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: properties,
                                 body: body);
        }
    }
}
