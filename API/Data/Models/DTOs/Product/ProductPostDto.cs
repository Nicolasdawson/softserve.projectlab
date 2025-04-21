using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs.Product
{
    public class ProductPostDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El detalle del producto es obligatorio")]
        public string Detail { get; set; }

        [Required(ErrorMessage = "El Precio del producto es obligatorio")]
        public decimal Price { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "La categoría del producto es obligatoria")]
        public int CategoryId { get; set; }
    }
}
