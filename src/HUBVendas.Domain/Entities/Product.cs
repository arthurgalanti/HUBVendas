using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;

namespace HUBVendas.Domain.Entities {
    public class Product : Entity {
        [Required]
        [JsonProperty("product_name")]
        public string Name { get; set; } = null!;
        [JsonProperty("product_description")]
        public string? Description { get; set; }
        [JsonProperty("sku")]
        public string? Sku { get; set; }
        [JsonProperty("bar_code")]
        public string? BarCode { get; set; }
        [JsonProperty("cost_price")]
        [Required]
        public decimal CostPrice { get; set; }
        [JsonProperty("selling_price")]
        [Required]
        public decimal SellingPrice { get; set; }
        [JsonProperty("stock")]
        [Required]
        public int Stock { get; set; }
        [JsonProperty("image")]
        public ProductImage? Image { get; set; }
        [JsonProperty("category")]
        public Category Category { get; set; } = null!;
    }

    public class ProductImage {
        [JsonProperty("image_name")]
        public string Name { get; set; } = null!;
        [JsonProperty("image_type")]
        public string Type { get; set; } = null!;
        [JsonProperty("image_base64")]
        public string Base64 { get; set; } = null!;

        public void SetImage(string name, string type, string base64) {
            Name = name;
            Type = type;
            Base64 = base64;
        }
    }

    public class ProductDTO : Notifiable<Notification> {
        [Required]
        public string Name { get; set; } = null!;
        [JsonProperty("product_description")]
        public string? Description { get; set; }
        [JsonProperty("sku")]
        public string? Sku { get; set; }
        [JsonProperty("bar_code")]
        public string? BarCode { get; set; }
        [Required]
        [JsonProperty("cost_price")]
        public decimal CostPrice { get; set; }
        [Required]
        [JsonProperty("selling_price")]
        public decimal SellingPrice { get; set; }
        [JsonProperty("stock")]
        public int Stock { get; set; }
        [JsonProperty("image")]
        public ProductImage? Image { get; set; }
        [JsonProperty("category_id")]
        public Guid CategoryId { get; set; }

        public void Validate() {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, "Nome", "O Nome do produto é obrigatório!")
                .IsNotNullOrEmpty(CategoryId.ToString(), "IDCategoria", "O ID da categoria é obrigatório!")
        );
        }
    }
}