using Flunt.Notifications;
using Flunt.Validations;

namespace HUBVendas.Domain.Entities {
    public class Category : Entity {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CategoryDTO : Notifiable<Notification> {
        public string Name { get; set; } = null!;
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