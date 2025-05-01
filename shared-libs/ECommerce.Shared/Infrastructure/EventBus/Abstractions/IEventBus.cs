using ECommerce.Shared.Infrastructure.EventBus;

namespace ECommerce.Shared.Infrastructure.EventBus.Abstractions;
public interface IEventBus
{
  Task PublishAsync(Event @event);
}