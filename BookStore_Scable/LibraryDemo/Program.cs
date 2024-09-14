using LibraryDemo;
using LibraryDemo.DataAccess;
using LibraryDemo.DBContext;
using LibraryDemo.Handlers.GetAllBooks;
using LibraryDemo.Models;
using LibraryDemo.Queries.GetAllBooks;
using LibraryDemo.Queries.GetAllUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;


//var DbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var DbName= Environment.GetEnvironmentVariable("DB_NAME");
//var DbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); 


class Program
{
    public static void Main(string[] args)
    {
        // 1. Configure services
        var services = new ServiceCollection();

        // 2. Add MediatR services
        //services.AddMediatR(options =>
        //    {
        //        options.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        //    });

        // 2. Add MediatR services
        services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(typeof(GetAllBooksHandler).Assembly);
            });
        services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(typeof(GetAllUserQuery).Assembly);
            });


        // 3. Build service provider
        var serviceProvider = services.BuildServiceProvider();

        // 4. Resolve and use the IMediator service
        var mediator = serviceProvider.GetService<IMediator>();

        Cart cart = new()
        {
            Id = 1,
            User = new User() { Name = "User" },
            UserId = 1,
        };
        string json = JsonSerializer.Serialize(cart);
        DemoDataAccess demoData = new();
        demoData.AddCartToQueue(json);

        //List<Author> authorList = new List<Author>();   
        //Author author = new Author()
        //{
        //    Name = "Author_Test1"
        //};
        //authorList.Add(author);
        //BooksStoreDbContext dbContext = new BooksStoreDbContext();
        ////dbContext.Authors.Add(author);
        ////dbContext.SaveChanges();

        //var user = dbContext.Users.FirstOrDefault();

        //List<Book> books = new List<Book>();
        //var BookSample = new Book()
        //{
        //    Title = "Test",
        //    Authors = authorList
        //};
        //books.Add(BookSample);

        //var a = new Cart
        //{
        //    User = user,
        //    UserId = user.Id,
        //    Books = books
        //};
        //dbContext.Carts.Add(a);
        //dbContext.SaveChanges();

        //var aada = mediator.Send(new GetAllUserQuery(new LibraryDemo.Models.FromBody.GetAllUserApiBody { }));
        //String JSON = JsonSerializer.Serialize(a);
        //Console.WriteLine(JSON);
        Console.ReadLine();

    }
}
//static void Main(string[] args)
//{
//    // Set up DI
//    var serviceCollection = new ServiceCollection();
//    ConfigureServices(serviceCollection);

//    var serviceProvider = serviceCollection.BuildServiceProvider();


//}

//private static void ConfigureServices(IServiceCollection services)
//{
//    // Register services here
//    services.AddSingleton<IDemoDataAccess, DemoDataAccess>();
//    services.AddMediatR(options =>
//    {
//        options.RegisterServicesFromAssemblies(typeof(Program).Assembly);
//    });


