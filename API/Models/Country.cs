using API.Abstractions;

namespace API.Models
{
    public class Country : Base
    {
        public string Name { get; set; } = default!;

        //Navigation properties
        // One Country has many Regions
        public ICollection<Region> Regions { get; set; } = new List<Region>();

    }
}
