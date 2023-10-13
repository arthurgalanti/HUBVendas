using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public class ProductDTO {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public ProductImage? Image { get; set; }

        public Guid CategoryId { get; set; }
    }
}