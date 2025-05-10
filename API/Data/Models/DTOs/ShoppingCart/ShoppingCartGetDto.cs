namespace API.Data.Models.DTOs.ShoppingCart
{
    public class ShoppingCartGetDto
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int Qty { get; set; }
    }
}
