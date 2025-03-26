using System.Collections.Generic;
using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin
{
    /// <summary>
    /// Implements the IItem interface for managing item data.
    /// </summary>
    public class Item : IItem
    {
        // Properties matching the diagram
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Sku { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int OriginalStock { get; set; }
        public int CurrentStock { get; set; }
        public string ItemCurrency { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MarginGain { get; set; }
        public decimal ItemDiscount { get; set; }
        public decimal AdditionalTax { get; set; }
        public decimal ItemPrice { get; set; }
        public bool ItemStatus { get; set; }
        public int CategoryId { get; set; }
        public string ItemImage { get; set; }

        // Minimal constructor, no extra comments to avoid redundancy
        public Item(int sku, string itemName, string itemDescription, int originalStock, int currentStock,
                    string itemCurrency, decimal unitCost, decimal marginGain, decimal itemDiscount,
                    decimal additionalTax, decimal itemPrice, bool itemStatus, int categoryId, string itemImage)
        {
            Sku = sku;
            ItemName = itemName;
            ItemDescription = itemDescription;
            OriginalStock = originalStock;
            CurrentStock = currentStock;
            ItemCurrency = itemCurrency;
            UnitCost = unitCost;
            MarginGain = marginGain;
            ItemDiscount = itemDiscount;
            AdditionalTax = additionalTax;
            ItemPrice = itemPrice;
            ItemStatus = itemStatus;
            CategoryId = categoryId;
            ItemImage = itemImage;
        }

        // Default constructor
        public Item() { }

        // CRUD methods (minimal comments)

        public Result<IItem> AddItem(IItem item)
        {
            return Result<IItem>.Success(item);
        }

        public Result<IItem> UpdateItem(IItem item)
        {
            return Result<IItem>.Success(item);
        }

        public Result<IItem> GetItemBySku(int sku)
        {
            // Example item
            var exampleItem = new Item(
                sku,
                "Example Item",
                "Description of example item",
                50,
                50,
                "USD",
                10.00m,
                0.20m,
                0.05m,
                0.10m,
                12.00m,
                true,
                1,
                "item.png"
            );
            return Result<IItem>.Success(exampleItem);
        }

        public Result<List<IItem>> GetAllItems()
        {
            var items = new List<IItem>
            {
                new Item(1001, "Item 1", "Description 1", 100, 100, "USD", 10.00m, 0.20m, 0.05m, 0.10m, 12.00m, true, 1, "item1.png"),
                new Item(1002, "Item 2", "Description 2", 200, 200, "USD", 20.00m, 0.25m, 0.10m, 0.15m, 25.00m, true, 2, "item2.png")
            };
            return Result<List<IItem>>.Success(items);
        }

        public Result<bool> RemoveItem(int sku)
        {
            return Result<bool>.Success(true);
        }

        // Methods from the diagram

        public Result<bool> UpdatePrice()
        {
            // TODO: Logic to update the price
            // Example: this.ItemPrice = newPrice;
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateDiscount()
        {
            // TODO: Logic to update the discount
            // Example: this.ItemDiscount = newDiscount;
            return Result<bool>.Success(true);
        }
    }
}
