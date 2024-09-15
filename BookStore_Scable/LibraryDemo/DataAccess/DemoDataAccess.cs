using LibraryDemo.DBContext;
using LibraryDemo.Models;
using LibraryDemo.Models.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;

namespace LibraryDemo.DataAccess
{
    public class DemoDataAccess : IDemoDataAccess
    {
        //Get User By Id
        public async Task<User?> GetUserById(int id)
        {
            BooksStoreDbContext dbContext = new();
            var user = await dbContext.Users
                .Include(e => e.Cart)
                .FirstOrDefaultAsync(e => e.Id == id);

            return user;
        }

        //Get All User
        public IEnumerable<object> GetAllUsers(int skipCount = 0, int maxResult = 10, string? name = "")
        {
            BooksStoreDbContext dbContext = new();
            if (string.IsNullOrEmpty(name))
            {
                var users = dbContext.Users
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

        public async Task<UserFoundState> AddBoodToCartByUserId(int userId, BookDto book)
        {
            BooksStoreDbContext db = new();

            var user = db.Users.Include(e => e.Cart).Where(e => e.Id == userId).FirstOrDefault();

            if (user == null)
            {
                return UserFoundState.NotFound;
            }

            var bookSelected = db.Books
               .Include(e => e.Authors)
               .Include(e => e.Carts)
               .Where(e => e.Title == book.Title)
               .Distinct()
               .FirstOrDefault();

            if (bookSelected == null)
            {
                return UserFoundState.NotFound;
            }

            bool flag = false;
            foreach (var item in book.Authors)
            {
                foreach (var item2 in bookSelected.Authors)
                {
                    if (item2.Name == item)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }

            if (!flag)
            {
                return UserFoundState.NotFound_WithThisSpecification;
            }

            Cart cart = new Cart()
            {
                User = user,
                UserId = userId,
            };

            if (cart.Books == null)
            {
                cart.Books = new List<Book>();
            }
            cart.Books.Add(bookSelected);

            string jsonString = JsonSerializer.Serialize(cart);

            AddCartToQueue(jsonString);

            return UserFoundState.Ok;
        }
    }
}
