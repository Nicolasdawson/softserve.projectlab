namespace API.Data.Models.DTOs.ShoppingCart
{
    public class ShoppingCartPostDto
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int UserId { get; set; }
    }
}
