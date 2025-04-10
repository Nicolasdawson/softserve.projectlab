namespace softserve.projectlabs.Shared.DTOs
{
    public class AddItemToWarehouseDTO
    {
        public int Sku { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal Price { get; set; }
        public int OriginalStock { get; set; }
        public int CurrentStock { get; set; }
        public string ItemCurrency { get; set; } = "USD";
        public decimal UnitCost { get; set; }
        public decimal MarginGain { get; set; }
        public decimal? ItemDiscount { get; set; }
        public decimal? AdditionalTax { get; set; }
        public decimal ItemPrice { get; set; }
        public bool ItemStatus { get; set; } = true;
        public int CategoryId { get; set; }
        public string ItemImage { get; set; }

    }
}
