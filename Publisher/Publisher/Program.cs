﻿// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
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

Console.WriteLine("Digite sua mensagem e aperte <ENTER>");

while (true)
{
    string? message = Console.ReadLine();

    if (message is null || message == "")
        break;

    var aluno = new Aluno() { Id = 1, Nome = "Milton" };
    message = JsonSerializer.Serialize(aluno);

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "", 
        routingKey: "saudacao_1", 
        basicProperties: null,
        body: body);

    Console.WriteLine($"[x] Enviada: {message}");
}


class Aluno
{
    public int Id { get; set; }
    public string? Nome { get; set; }
}



