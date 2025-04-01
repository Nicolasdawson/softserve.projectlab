using System;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs
{
    public class ClientDtoIn
    {
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [MaxLength(100, ErrorMessage = "Los apellidos no pueden exceder los 100 caracteres.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de cumpleaños es obligatoria")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [MaxLength(10, ErrorMessage = "El número de celular no puede exceder los 10 digitos.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [MaxLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres.")]
        public string Address { get; set; } = null!;
    }
}
