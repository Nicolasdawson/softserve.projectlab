namespace softserve.projectlabs.Shared.DTOs.Item;

public class ItemCreateDto
{
    public int Sku { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public int OriginalStock { get; set; }
    public string ItemCurrency { get; set; } = string.Empty;
    public decimal ItemUnitCost { get; set; }
    public decimal ItemMarginGain { get; set; }
    public decimal? ItemDiscount { get; set; }
    public decimal? ItemAdditionalTax { get; set; }
    public decimal ItemPrice { get; set; }
    public int CategoryId { get; set; }
    public string? ItemImage { get; set; }
}
