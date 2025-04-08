using API.Abstractions;

namespace API.Models
{
    public class Region : Base
    {
        public string Name { get; set; } = default!;

        // Foreign Key: IdCountry
        public Guid IdCountry { get; set; }

        // Navigation Property
        public Country Country { get; set; } = default!;

        // One Region has many Cities
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
