using System;
namespace HUBVendas.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        public bool Active { get; set; } = true;
        public bool Removed { get; set; } = false;
    }
}