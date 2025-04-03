using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs
{
    public class ProductCategoryDtoIn
    {
        [Required(ErrorMessage = "El Nombre de la categoria es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre de la categoria no puede exceder los 100 caracteres.")]
        public string category { get; set; }
    }
}
