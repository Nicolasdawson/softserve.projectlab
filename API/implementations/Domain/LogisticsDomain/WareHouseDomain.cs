using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Data.Entities;
using API.Models;


namespace API.implementations.Domain
{
    public class WarehouseDomain
    {
        private readonly IWarehouse _warehouse;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseDomain"/> class.
        /// </summary>
        /// <param name="warehouse">The warehouse interface.</param>
        public WarehouseDomain(IWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        /// <summary>
        /// Adds an item to the warehouse.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A result indicating success or failure.</returns>
        public Result<IWarehouse> AddItem(Item item)
        {
            try
            {
                _warehouse.AddItem(item);
                return Result<IWarehouse>.Success(_warehouse);
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Error adding item: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes an item from the warehouse.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>A result indicating success or failure.</returns>
        public Result<IWarehouse> RemoveItem(Item item)
        {
            try
            {
                _warehouse.RemoveItem(item);
                return Result<IWarehouse>.Success(_warehouse);
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Error removing item: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the available stock for a specific SKU.
        /// </summary>
        /// <param name="sku">The SKU to check.</param>
        /// <returns>A result indicating success or failure.</returns>
        public Result<IWarehouse> GetAvailableStock(int sku)
        {
            try
            {
                var stock = _warehouse.GetAvailableStock(sku);
                return Result<IWarehouse>.Success(_warehouse); // Or return `stock` instead
            }
            catch (Exception ex)
            {
                return Result<IWarehouse>.Failure($"Error getting available stock: {ex.Message}");
            }
        }
    }
}
