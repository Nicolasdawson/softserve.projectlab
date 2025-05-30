namespace API.DTO;

    public class CountryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
