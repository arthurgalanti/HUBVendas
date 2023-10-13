using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;

namespace HUBVendas.Domain.Entities {
    public class Product : Entity {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public ProductImage? Image { get; set; }

        public Category Category { get; set; } = null!;
    }

    public class ProductImage {
        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Base64 { get; set; } = null!;

        public void SetImage(string name, string type, string base64) {
            Name = name;
            Type = type;
            Base64 = base64;
        }
    }

    public class ProductDTO : Notifiable<Notification> {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public ProductImage? Image { get; set; }

        public Guid CategoryId { get; set; }

        public void Validate() {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Nome", "O Nome do produto é obrigatório!")
                .IsNotNullOrEmpty(CategoryId.ToString(), "IDCategoria", "O ID da categoria é obrigatório!")
                .IsGreaterThan(UnitPrice, 0, "Preço", "Preço minímo obrigatorio!")
                .IsGreaterOrEqualsThan(Quantity, 0, "Quantidade", "Quantidade miníma obrigatoria!")
        );
        }
    }
}