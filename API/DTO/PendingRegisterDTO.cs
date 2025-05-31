using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class PendingRegisterDTO
    {       
        public string Email { get; set; } = default!;       
        public string FirstName { get; set; } = default!;        
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public Guid? IdCustomer { get; set; }

        [Required]
        public string VerificationToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
