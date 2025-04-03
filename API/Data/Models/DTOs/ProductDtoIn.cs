using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs
{
    public class ProductDtoIn
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string name { get; set; }

        [Required(ErrorMessage = "La marca del producto es obligatoria")]
        [MaxLength(100, ErrorMessage = "La marca no puede exceder los 100 caracteres.")]
        public string brand { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria")]
        [MaxLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
        public string description { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 9999999999999999.99.")]
        public decimal Price { get; set; }

        //relacion con la tabla ProductCategory
        [Required(ErrorMessage = "La categoria del producto es obligatoria")]
        public int categoryId { get; set; }
    }
}
