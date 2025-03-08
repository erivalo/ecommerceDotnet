using Microsoft.EntityFrameworkCore;

namespace Order.Service.Infrastructure.Data.EntityFramework;
public static class EntityFrameworkExtensions
{
  public static void AddSqlServerDataStore(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
    services.AddScoped<IOrderStore, OrderContext>();
  }
}