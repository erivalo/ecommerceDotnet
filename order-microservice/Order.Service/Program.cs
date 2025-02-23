using Order.Service.Infrastructure.Data;
using Order.Service.Endpoints;
using ECommerce.Shared.Infrastructure.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderStore, InMemoryOrderStore>();
builder.Services
  .AddRabbitMqEventBus(builder.Configuration)
  .AddRabbitMqEventPublisher();

var app = builder.Build();

app.RegisterEndpoints();

app.Run();
