using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs.User
{
    public class UserRegisterDto
    {
        //[Required(ErrorMessage = "El nombre es obligatorio")]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "El correo electrónico es obligatorio")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "El password es obligatorio")]
        //public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
