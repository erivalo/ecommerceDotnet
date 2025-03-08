using Order.Service.Infrastructure.Data;
using Order.Service.Endpoints;
using ECommerce.Shared.Infrastructure.RabbitMq;
using Order.Service.Infrastructure.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<IOrderStore, InMemoryOrderStore>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlServerDataStore(builder.Configuration);

builder.Services
  .AddRabbitMqEventBus(builder.Configuration)
  .AddRabbitMqEventPublisher();

var app = builder.Build();

app.RegisterEndpoints();

app.UseHttpsRedirection();

app.Run();
