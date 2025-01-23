// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");


var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "saudacao_1",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

Console.WriteLine(" [*] Aguardando mensagens.");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    var aluno = JsonSerializer.Deserialize<Aluno>(message);

    Console.WriteLine($" [X] Recebida: {message}");
};

channel.BasicConsume(queue: "saudacao_1",
                     autoAck: true, 
                     consumer: consumer);

Console.WriteLine(" Aperte [enter] para sair.");
Console.ReadLine();

class Aluno
{
    public int Id { get; set; }
    public string? Nome { get; set; }
}


