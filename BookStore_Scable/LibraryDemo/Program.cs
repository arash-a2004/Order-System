using LibraryDemo;
using LibraryDemo.DataAccess;
using LibraryDemo.Handlers;
using LibraryDemo.Models;
using LibraryDemo.Queries.GetAllBooks;
using MediatR;
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


        // 3. Build service provider
        var serviceProvider = services.BuildServiceProvider();

        // 4. Resolve and use the IMediator service
        var mediator = serviceProvider.GetService<IMediator>();

        var a = mediator.Send(new GetAllBooksQuery(new ApiBook()));
        String JSON = JsonSerializer.Serialize(a);
        Console.WriteLine(JSON);
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


