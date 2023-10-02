namespace HUBVendas.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CategoryDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}