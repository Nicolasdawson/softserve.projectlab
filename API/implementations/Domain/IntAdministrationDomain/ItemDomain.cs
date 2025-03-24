using API.Models;
using API.Models.IntAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    /// <summary>
    /// Domain class for handling Item operations.
    /// Uses in-memory storage for items.
    /// </summary>
    public class ItemDomain
    {
        // In-memory storage for items
        private readonly List<Item> _items = new List<Item>();

        /// <summary>
        /// Creates a new item and adds it to the in-memory list.
        /// </summary>
        /// <param name="item">Item object to be created</param>
        /// <returns>Result object containing the created item</returns>
        public async Task<Result<Item>> CreateItem(Item item)
        {
            try
            {
                _items.Add(item);
                return Result<Item>.Success(item);
            }
            catch (Exception ex)
            {
                return Result<Item>.Failure($"Failed to create item: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing item if found.
        /// </summary>
        /// <param name="item">Item object with updated information</param>
        /// <returns>Result object containing the updated item</returns>
        public async Task<Result<Item>> UpdateItem(Item item)
        {
            try
            {
                var existingItem = _items.FirstOrDefault(i => i.Sku == item.Sku);
                if (existingItem != null)
                {
                    existingItem.ItemName = item.ItemName;
                    existingItem.ItemDescription = item.ItemDescription;
                    existingItem.OriginalStock = item.OriginalStock;
                    existingItem.CurrentStock = item.CurrentStock;
                    existingItem.ItemCurrency = item.ItemCurrency;
                    existingItem.UnitCost = item.UnitCost;
                    existingItem.MarginGain = item.MarginGain;
                    existingItem.ItemDiscount = item.ItemDiscount;
                    existingItem.AdditionalTax = item.AdditionalTax;
                    existingItem.ItemPrice = item.ItemPrice;
                    existingItem.ItemStatus = item.ItemStatus;
                    existingItem.CategoryId = item.CategoryId;
                    existingItem.ItemImage = item.ItemImage;
                    return Result<Item>.Success(existingItem);
                }
                else
                {
                    return Result<Item>.Failure("Item not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Item>.Failure($"Failed to update item: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves an item by its SKU.
        /// </summary>
        /// <param name="sku">Unique identifier of the item</param>
        /// <returns>Result object containing the item if found, otherwise an error</returns>
        public async Task<Result<Item>> GetItemBySku(int sku)
        {
            try
            {
                var item = _items.FirstOrDefault(i => i.Sku == sku);
                return item != null
                    ? Result<Item>.Success(item)
                    : Result<Item>.Failure("Item not found.");
            }
            catch (Exception ex)
            {
                return Result<Item>.Failure($"Failed to get item: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all items stored in memory.
        /// </summary>
        /// <returns>Result object containing a list of items</returns>
        public async Task<Result<List<Item>>> GetAllItems()
        {
            try
            {
                return Result<List<Item>>.Success(_items);
            }
            catch (Exception ex)
            {
                return Result<List<Item>>.Failure($"Failed to retrieve items: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes an item by its SKU.
        /// </summary>
        /// <param name="sku">Unique identifier of the item to remove</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> RemoveItem(int sku)
        {
            try
            {
                var itemToRemove = _items.FirstOrDefault(i => i.Sku == sku);
                if (itemToRemove != null)
                {
                    _items.Remove(itemToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Item not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove item: {ex.Message}");
            }
        }
    }
}
