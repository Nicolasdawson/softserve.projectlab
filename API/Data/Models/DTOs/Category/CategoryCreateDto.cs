using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        public string Name { get; set; }
    }
}
