using System.Text.Json;
using ECommerce.Shared.Infrastructure.EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Shared.Infrastructure.Outbox;
public class OutboxContext : DbContext, IOutboxStore
{
  public OutboxContext(DbContextOptions<OutboxContext> options) : base(options)
  {
  }
  public DbSet<OutboxEvent> OutboxEvents { get; set; }

  public async Task AddOutboxEvent<T>(T @event) where T : Event
  {
    var existingEvent = await OutboxEvents.FindAsync(@event.Id);
    if (existingEvent is not null) return;
    OutboxEvents.Add(new OutboxEvent
    {
      Id = @event.Id,
      EventType = @event.GetType().AssemblyQualifiedName,
      Data = JsonSerializer.Serialize(@event),
      Sent = false
    });
    await SaveChangesAsync();
  }

  public IExecutionStrategy CreateExecutionStrategy() => Database.CreateExecutionStrategy();

  public Task<List<OutboxEvent>> GetUnpublishedOutboxEvents()
    => OutboxEvents.Where(o => !o.Sent).ToListAsync();

  public async Task MarkOutboxEventAsPublished(Guid outboxEventId)
  {
    var outboxEvent = await OutboxEvents.FindAsync(outboxEventId);
    if (outboxEvent is not null)
    {
      outboxEvent.Sent = true;
      await SaveChangesAsync();
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new OutboxEventConfiguration());
  }
}