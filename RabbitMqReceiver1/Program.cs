using LibraryDemo.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

ConnectionFactory connectionFactory = new();
//TODO : from appSetting
var factory = new ConnectionFactory { HostName = "amqp://guest:guest@localhost:15672" };
factory.ClientProvidedName = "Rabbit Receiver1 App";

IConnection connection = connectionFactory.CreateConnection();

IModel channel = connection.CreateModel();

string exchangeName = "DemoExchange";
string routeKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routeKey, null);
channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();

    string message = Encoding.UTF8.GetString(body);

    Cart cart = new Cart();

    cart = JsonSerializer.Deserialize<Cart>(message);

    Console.WriteLine($"Message Received : {message}");

    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();
connection.Close();
