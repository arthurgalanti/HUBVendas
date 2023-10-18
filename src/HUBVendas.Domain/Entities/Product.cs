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
        
        public string? Sku { get; set; }

        public string? BarCode { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Stock { get; set; }

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
        
        public string? Sku { get; set; }

        public string? BarCode { get; set; }

        public decimal CostPrice { get; set; }
        
        public decimal SellingPrice { get; set; }

        public int Stock { get; set; }

        public ProductImage? Image { get; set; }

        public Guid CategoryId { get; set; }

        public void Validate() {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Nome", "O Nome do produto é obrigatório!")
                .IsNotNullOrEmpty(CategoryId.ToString(), "IDCategoria", "O ID da categoria é obrigatório!")
                .IsGreaterThan(SellingPrice, 0, "Valor de venda", "Valor de venda obrigatorio!")
                .IsGreaterOrEqualsThan(Stock, 0, "Estoque", "Estoque minímo obrigatorio!")
        );
        }
    }
}