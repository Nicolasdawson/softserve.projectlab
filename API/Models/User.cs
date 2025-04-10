using API.Abstractions;

namespace API.Models
{
    public class User : Base
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        //ForeignKey: IdRole
        public Guid IdRole { get; set; }

        //Navigation Property
        public Role Role { get; set; } = default!;
    }
}
