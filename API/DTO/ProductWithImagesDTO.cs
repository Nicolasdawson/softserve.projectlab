namespace API.DTO
{
    public class ProductWithImagesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }        
        public int Stock { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public string CategoryName { get; set; } = default!;
    }
}
