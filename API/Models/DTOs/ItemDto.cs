namespace API.Models.DTOs
{
    public class ItemDto
    {
        public int Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }
        public int CurrentStock { get; set; }
    }
}
