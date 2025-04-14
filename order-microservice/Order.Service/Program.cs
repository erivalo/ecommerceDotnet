using Order.Service.Endpoints;
using ECommerce.Shared.Infrastructure.RabbitMq;
using Order.Service.Infrastructure.Data.EntityFramework;
using ECommerce.Shared.Observability;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<IOrderStore, InMemoryOrderStore>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlServerDataStore(builder.Configuration);

builder.Services
  .AddRabbitMqEventBus(builder.Configuration)
  .AddRabbitMqEventPublisher();
const string serviceName = "Order";
builder.Services.AddOpenTelemetryTracing(
  serviceName,
  builder.Configuration,
  traceBuilder => traceBuilder.WithSqlInstrumentation())
  .AddOpenTelemetryMetrics(serviceName, builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MigrateDatabase();
}

app.RegisterEndpoints();

app.UseHttpsRedirection();

app.Run();
