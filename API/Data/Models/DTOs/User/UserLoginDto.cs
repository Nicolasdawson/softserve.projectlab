using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
