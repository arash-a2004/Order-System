using RabbitMQ.Client;
using RestApiServices.Models;
using System.Text;


namespace RestApiServices.Services
{
    public class AddCartToRabbitQueue
    {

        public static void AddCartToQueue(string CartjsonSerialize)
        {
            ConnectionFactory connectionFactory = new();
            //TODO : from appSetting
            var factory = new ConnectionFactory { HostName = "amqp://guest:guest@localhost:15672" };
            factory.ClientProvidedName = "Rabbit Sender App";
            
            IConnection connection = connectionFactory.CreateConnection();
            
            IModel channel = connection.CreateModel();

            string exchangeName = "DemoExchange";
            string routeKey = "demo-routing-key";
            string queueName = "DemoQueue";

            channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
            channel.QueueDeclare(queueName,false,false,false,null);
            channel.QueueBind(queueName,exchangeName,routeKey,null);

            byte[] messagBody = Encoding.UTF8.GetBytes(CartjsonSerialize);
            channel.BasicPublish(exchangeName, routeKey, null, messagBody);

            channel.Close();
            connection.Close();
        }


    }
}
