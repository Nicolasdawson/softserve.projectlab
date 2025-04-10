using API.Abstractions;

namespace API.Models
{
    public class Customer : Base
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        // Foreign Key: IdUser
        public Guid IdUser { get; set; }

        // Navigation Property
        public User User { get; set; } = default!;
    }
}
