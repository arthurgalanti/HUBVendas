using Newtonsoft.Json;

namespace HUBVendas.Domain.Entities {
    public abstract class Entity {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(-3);
        [JsonProperty("fl_active")]
        public bool Active { get; set; } = true;
    }
}