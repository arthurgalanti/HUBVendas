using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;

namespace HUBVendas.Domain.Entities {
    public class Category : Entity {
        [Required]
        [JsonProperty("category_name")]
        public string Name { get; set; } = null!;
        [JsonProperty("category_description")]
        public string? Description { get; set; }
    }

    public class CategoryDTO : Notifiable<Notification> {
        [Required]
        [JsonProperty("category_name")]
        public string Name { get; set; } = null!;
        [JsonProperty("category_description")]
        public string? Description { get; set; }

        public void Validate() {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Nome", "O Nome da categoria é obrigatório!")
        );
        }
    }
}