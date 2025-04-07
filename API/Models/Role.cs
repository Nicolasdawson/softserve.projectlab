using API.Abstractions;

namespace API.Models
{
    public class Role : Base
    {
        public string Name { get; set; } = default!;

        // One role has many Users
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
