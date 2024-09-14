using LibraryDemo.DBContext;
using LibraryDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;

namespace LibraryDemo.DataAccess
{
    public class DemoDataAccess : IDemoDataAccess
    {
        private BooksStoreDbContext _DBContext { get; set; }

        public DemoDataAccess(BooksStoreDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        //Get User By Id
        public async Task<User?> GetUserById(int id)
        {
            var user = await _DBContext.Users
                .Include(e => e.Cart)
                .FirstOrDefaultAsync(e => e.Id == id);

            return user;
        }

        //Get All User
        public IEnumerable<object> GetAllUsers(int skipCount = 0, int maxResult = 10, string? name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                var users = _DBContext.Users
                    .Include(u => u.Cart)
                        .ThenInclude(e => e.Books)
                    .Select(u => new
                    {
                        Names = u.Name,
                        Titles = u.Cart.Books.Select(a => a.Title).ToList()
                    }).Skip(skipCount).Take(maxResult).ToList();

                return users;
            }
            return null;
        }
        //Add To Queue
        public void AddCartToQueue(string CartjsonSerialize)
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

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routeKey, null);

            byte[] messagBody = Encoding.UTF8.GetBytes(CartjsonSerialize);
            channel.BasicPublish(exchangeName, routeKey, null, messagBody);

            channel.Close();
            connection.Close();
        }
    }
}
