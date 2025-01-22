// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

Console.WriteLine("Hello, World!");


var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "saudacao_1",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        Console.WriteLine($" antes 1  111");
        consumer.Received += (model, ea) =>
        {
            Console.WriteLine($" antes");
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [X] Recebida: {message}");
        };
        Console.WriteLine($" depois");
        channel.BasicConsume(queue: "saudacao_1", autoAck: true, consumer: consumer);

        Console.ReadLine();
    }
