using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using API.Abstractions;

namespace API.Models;

    public class Product : Base
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


        //Navigation Properties
        [JsonIgnore] //Para evitar la referencia ciclica
        public Category? Category { get; set; } = default!;

        // One Product has many Images
        [JsonIgnore]
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        [NotMapped]
        [JsonIgnore] // Para evitar loops al serializar
        public Product? Prod { get; set; }
}
