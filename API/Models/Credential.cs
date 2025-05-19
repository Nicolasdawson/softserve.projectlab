using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using API.Abstractions;

namespace API.Models
{
    public class Credential : Base
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }


        //ForeignKey: IdRole
        public Guid IdRole { get; set; }

        //ForeignKey: IdCustomer
        public int IdCustomer { get; set; }

        //Navigation Property
        [JsonIgnore]
        public Role Role { get; set; } = default!;

        [NotMapped]
        [JsonIgnore]
        public Customer Customer { get; set; } = default!;        
    }
}
