using MediatR.Sample.Core.Entities;

namespace MediatR.Sample.Core.Events
{
    public class EntityCreated<T> : INotification where T : Entity
    {
        public EntityCreated(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}
