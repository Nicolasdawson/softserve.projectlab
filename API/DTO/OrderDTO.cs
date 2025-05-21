namespace API.DTO.Order;

    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public Guid DeliveryAddressId { get; set; }
        public List<OrderProductDto> Products { get; set; } = new();
    }

    public class OrderProductDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderFromCartRequest
    {
        public int CustomerId { get; set; }
        public Guid DeliveryAddressId { get; set; }
    }

    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = default!;
        public decimal Total { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }

    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = default!;
        public decimal Total { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DeliveryAddressDto DeliveryAddress { get; set; } = default!;
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class DeliveryAddressDto
    {
        public Guid Id { get; set; }
        public string StreetName { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;
        public string StreetNameOptional { get; set; } = default!;
        public CityDto City { get; set; } = default!;
    }

    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }



