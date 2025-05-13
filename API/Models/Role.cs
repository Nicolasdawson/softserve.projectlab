using API.Abstractions;

namespace API.Models
{
    public class Role : Base
    {
        public string Name { get; set; } = default!;

        // One role has many Users credentials
        public ICollection<Credential> credentials { get; set; } = new List<Credential>();

    }
}
