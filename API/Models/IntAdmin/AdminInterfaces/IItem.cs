using API.Data.Entities;
using System.Collections.Generic;

namespace API.Models.IntAdmin.Interfaces
{
    /// <summary>
    /// Represents an item with methods for CRUD operations.
    /// </summary>
    public interface IItem
    {
        int Sku { get; set; }
        //string ItemName { get; set; }
        //string ItemDescription { get; set; }
        int OriginalStock { get; set; }
        int CurrentStock { get; set; }
        string ItemCurrency { get; set; }
        decimal UnitCost { get; set; }
        decimal MarginGain { get; set; }
        decimal ItemDiscount { get; set; }
        decimal AdditionalTax { get; set; }
        decimal ItemPrice { get; set; }
        bool ItemStatus { get; set; }
        int CategoryId { get; set; }
        string ItemImage { get; set; }

        // CRUD methods
        Result<IItem> AddItem(IItem item);
        Result<IItem> UpdateItem(IItem item);
        Result<IItem> GetItemBySku(int sku);
        Result<List<IItem>> GetAllItems();
        Result<bool> RemoveItem(int sku);

        // Methods from the diagram
        Result<bool> UpdatePrice();
        Result<bool> UpdateDiscount();
    }
}
