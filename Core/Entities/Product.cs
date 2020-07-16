using System;

namespace MediatR.Sample.Core.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public Guid? CategoryId { get; set; }
    }
}