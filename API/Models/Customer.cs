using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Customer
{
    [Key]  // Esto indica que 'Id' es la clave primaria
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    // Atributos que no se usaran o lo haran en otra entidad
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; }
    public LineOfCredit? LineOfCredit { get; set; }
    
}