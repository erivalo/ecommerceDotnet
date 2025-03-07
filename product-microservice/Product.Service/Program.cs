using ECommerce.Shared.Infrastructure.RabbitMq;
using Product.Service.Endpoints;
using Product.Service.Infrastructure.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddRabbitMqEventBus(builder.Configuration)
  .AddRabbitMqEventPublisher();
builder.Services.AddSqlServerDatastore(builder.Configuration);

var app = builder.Build();

app.RegisterEndpoints();

if (app.Environment.IsDevelopment())
{
  app.MigrateDatabase();
}

app.Run();
