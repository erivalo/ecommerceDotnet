using Basket.Service.Endpoints;
using Basket.Service.Infrastructure.Data;
using ECommerce.Shared.Infrastructure.RabbitMq;
using Basket.Service.IntegrationEvents;
using Basket.Service.IntegrationEvents.EventHandlers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBasketStore, InMemoryBasketStore>();
builder.Services.AddRabbitMqEventBus(builder.Configuration)
  .AddRabbitSubscriberService(builder.Configuration)
  .AddEventHandler<OrderCreatedEvent, OrderCreatedEventHandler>();
// builder.Services.AddHostedService<RabbitMqHostedService>();

var app = builder.Build();

app.RegisterEndpoints();

app.Run();
