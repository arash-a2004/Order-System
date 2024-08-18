using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitSender;

//create service collection for DI
var serviceCollection = new ServiceCollection();

//Build configuration
IConfiguration configuration;
configuration = new  ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    .AddJsonFile("appsetting.json")
    .Build();

//add configuration to service collection 
serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddSingleton<UserPass>();

//test
var serviceProvider = serviceCollection.BuildServiceProvider();
var testInstance = serviceProvider.GetService<UserPass>();
testInstance.GetData();

