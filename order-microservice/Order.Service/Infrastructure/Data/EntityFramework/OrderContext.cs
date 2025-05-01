
using Microsoft.EntityFrameworkCore;

namespace Order.Service.Infrastructure.Data.EntityFramework;
internal class OrderContext : DbContext, IOrderStore
{
  public OrderContext(DbContextOptions<OrderContext> options)
    : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new OrderConfiguration());
    modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
  }

  public DbSet<Models.Order> Orders { get; set; }
  public DbSet<Models.OrderProduct> OrderProducts { get; set; }
  public async Task CreateOrder(Models.Order order)
  {
    Orders.Add(order);
    await SaveChangesAsync();
  }

  public async Task<Models.Order?> GetCustomerOrderById(string customerId, string orderId)
  {
    return await Orders
      .FirstOrDefaultAsync(o =>
        o.CustomerId == customerId
        && o.OrderId.ToString() == orderId);
  }
}