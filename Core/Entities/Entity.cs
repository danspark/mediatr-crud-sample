using System;

namespace MediatR.Sample.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
