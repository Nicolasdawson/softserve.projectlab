namespace API.Data.Models.DTOs
{
    public class ProductDtoOut
    {
        public int id { get; set; }

        public string name { get; set; }

        public string brand { get; set; }

        public string description { get; set; }

        public decimal Price { get; set; }

        public string ProductCategoryName { get; set; }
    }
}
