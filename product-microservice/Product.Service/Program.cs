using ECommerce.Shared.Authentication;
using ECommerce.Shared.Infrastructure.Outbox;
using ECommerce.Shared.Infrastructure.RabbitMq;
using ECommerce.Shared.Observability;
using Product.Service.Endpoints;
using Product.Service.Infrastructure.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutbox(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services
  .AddRabbitMqEventBus(builder.Configuration)
  .AddRabbitMqEventPublisher();
builder.Services.AddSqlServerDatastore(builder.Configuration);
builder.Services.AddOpenTelemetryTracing(
  "Product",
  builder.Configuration,
  (traceBuilder) => traceBuilder.WithSqlInstrumentation());

var app = builder.Build();

app.RegisterEndpoints();
app.UseJwtAuthentication();

if (app.Environment.IsDevelopment())
{
  app.MigrateDatabase();
  app.ApplyOutboxMigrations();
}

app.Run();
