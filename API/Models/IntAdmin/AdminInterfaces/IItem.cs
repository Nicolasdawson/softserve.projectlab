namespace API.Models.IntAdmin.Interfaces
{
    public interface IItem
    {
        string Sku { get; set; }
        string ProductName { get; set; }
        string ProductDescription { get; set; }
        int TotalQuantity { get; set; }
        decimal UnitCost { get; set; }
        string ProductCurrency { get; set; }
        decimal MarginGain { get; set; }
        decimal AdditionalTax { get; set; }
        decimal SalePrice { get; set; }
        decimal Discount { get; set; }
        string StatusProduct { get; set; }

        Result<IItem> AddItem(IItem item);
        Result<IItem> UpdateItem(IItem item);
        Result<IItem> GetItemBySku(string sku);
        Result<List<IItem>> GetAllItems();
        Result<bool> RemoveItem(string sku);
    }
}
