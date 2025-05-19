using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

public class Customer
{

    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public bool IsGuest { get; set; }

    // CDC/SCD2: StartDate (from), EndDate (to), IsCurrent (active record)
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
}
