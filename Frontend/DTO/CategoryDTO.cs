namespace Frontend.DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
    }
}
