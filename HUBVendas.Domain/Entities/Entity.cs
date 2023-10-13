using System;
using Flunt.Notifications;
using HUBVendas.Domain.Interfaces;

namespace HUBVendas.Domain.Entities {
    public abstract class Entity {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(-3);
        public bool Active { get; set; } = true;
        public bool Removed { get; set; } = false;

        public string CreatedOnString {
            get { return CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

    }
}