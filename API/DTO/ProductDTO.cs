using API.Abstractions;

namespace API.DTO
{
    public class ProductDTO : Base
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public int Stock { get; set; }

        // ForeignKey: Idcategory
        public Guid IdCategory { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
