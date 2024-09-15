using LibraryDemo.Handlers.AddCartToQueue;
using LibraryDemo.Handlers.GetAllBooks;
using LibraryDemo.Handlers.GetAllUser;
using LibraryDemo.Handlers.GetUserById;
using LibraryDemo.Queries.GetAllUser;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(GetAllBooksHandler).Assembly);
});

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(GetAllUsersHandlers).Assembly);
});

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(AddCartToQueueHandler).Assembly);
});

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(GetUserByIdHandler).Assembly);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////seeding data
//RestApiServices.FakeData.DataGenerator dbG = new();
//dbG.SeedData();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
