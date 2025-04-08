using API.Abstractions;

namespace API.Models
{
    public class ProductImage : Base
    {
        public string ImageUrl { get; set; } = default!;

        //ForeignKey: IdProduct
        public Guid IdProduct { get; set; }

        //Navigation Property
        public Product Product { get; set; } = default!;
    }
}
