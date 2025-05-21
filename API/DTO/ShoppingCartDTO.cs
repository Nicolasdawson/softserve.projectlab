namespace API.DTO.ShoppingCart;

    public class AddToCartRequest
    {
        public int CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItemResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;
    }

    public class UpdateCartQuantityRequest
    {
        public int CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }


