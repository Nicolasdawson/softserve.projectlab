using API.Abstractions;

namespace API.Models
{
    public class User : Base
    {
        public string Email { get; set; } = default!;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }


        //ForeignKey: IdRole
        public Guid IdRole { get; set; }

        //Navigation Property
        public Role Role { get; set; } = default!;
    }
}
