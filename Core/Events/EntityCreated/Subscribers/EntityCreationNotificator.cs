using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Events
{
    public class EntityCreationNotificator<T> : INotificationHandler<EntityCreated<T>> where T : Entity
    {
        private readonly DomainEventPublisher _eventPublisher;

        public EntityCreationNotificator(DomainEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Task Handle(EntityCreated<T> notification, CancellationToken cancellationToken)
        {
            return _eventPublisher.PublishAsync(new
            {
                event_type = "entity-created",
                event_data = notification.Entity
            }, cancellationToken);
        }
    }
}
