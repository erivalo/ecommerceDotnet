using Order.Service.Infrastructure.Data;
using Order.Service.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderStore, InMemoryOrderStore>();

var app = builder.Build();

app.RegisterEndpoints();

app.Run();
