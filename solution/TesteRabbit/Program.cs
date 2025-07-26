using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
Console.WriteLine("Conectado com sucesso!");
