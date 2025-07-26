using DesafioBTG.MS.Models;
using DesafioBTG.MS.Services;
using DesafioBTG.MS.Utils;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var configuration = AppSettings.Configuration;
var mongoSettings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
var rabbitSettings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

if (mongoSettings == null || string.IsNullOrWhiteSpace(mongoSettings.ConnectionString))
    throw new InvalidOperationException("Parâmetro MongoDB não encontrado.");

if (rabbitSettings == null || string.IsNullOrWhiteSpace(rabbitSettings.HostName))
    throw new InvalidOperationException("Parâmetro RabbitMQ não encontrado.");

var factory = new ConnectionFactory() { HostName = rabbitSettings.HostName };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: rabbitSettings.QueueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var mongoService = new MongoService(mongoSettings);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    ProcessarMensagem(message, mongoService);
};

channel.BasicConsume(queue: rabbitSettings.QueueName,
                     autoAck: true,
                     consumer: consumer);

void ProcessarMensagem(string message, MongoService mongoService)
{
    try
    {
        var pedido = JsonSerializer.Deserialize<Pedido>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (pedido?.CodigoPedido > 0)
        {
            mongoService.IncluirPedido(pedido);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Erro ao processar mensagem: {ex.Message}");
    }
}

Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Microsserviço aguardando mensagens da fila '{rabbitSettings.QueueName}'...");
Console.ReadLine();
