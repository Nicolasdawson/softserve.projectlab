using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class PendingRegistration
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Email { get; set; } = default!;

    [MaxLength(100)]
    public string FirstName { get; set; } = default!;

    [MaxLength(100)]
    public string LastName { get; set; } = default!;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = default!;

    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;

    
    public Guid? IdCustomer { get; set; }

    public string VerificationToken { get; set; }
    public DateTime Expiration { get; set; }
}
