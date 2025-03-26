using API.Models.Customers;
using API.Models.IntAdmin;

namespace API.Services.Customers;

/// <summary>
/// Provides operations for managing shopping carts.
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Gets a cart by ID.
    /// </summary>
    /// <param name="id">The ID of the cart to retrieve.</param>
    /// <returns>The cart if found; otherwise, null.</returns>
    Task<Cart?> GetCartByIdAsync(string id);

    /// <summary>
    /// Gets a customer's active cart.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The customer's active cart; creates a new one if none exists.</returns>
    Task<Cart> GetOrCreateCartForCustomerAsync(string customerId);

    /// <summary>
    /// Adds an item to a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="itemId">The ID of the item to add.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <returns>The updated cart if found; otherwise, null.</returns>
    Task<Cart?> AddItemToCartAsync(string cartId, string itemId, int quantity);

    /// <summary>
    /// Updates the quantity of an item in a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="itemId">The ID of the item to update.</param>
    /// <param name="quantity">The new quantity.</param>
    /// <returns>The updated cart if found; otherwise, null.</returns>
    Task<Cart?> UpdateCartItemQuantityAsync(string cartId, string itemId, int quantity);

    /// <summary>
    /// Removes an item from a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="itemId">The ID of the item to remove.</param>
    /// <returns>The updated cart if found; otherwise, null.</returns>
    Task<Cart?> RemoveItemFromCartAsync(string cartId, string itemId);

    /// <summary>
    /// Clears all items from a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart to clear.</param>
    /// <returns>The cleared cart if found; otherwise, null.</returns>
    Task<Cart?> ClearCartAsync(string cartId);

    /// <summary>
    /// Converts a cart to a package.
    /// </summary>
    /// <param name="cartId">The ID of the cart to convert.</param>
    /// <param name="packageName">The name for the new package.</param>
    /// <param name="contractTermMonths">The contract term in months.</param>
    /// <returns>The created package if the cart was found; otherwise, null.</returns>
    Task<Package?> ConvertCartToPackageAsync(string cartId, string packageName, int contractTermMonths);

    /// <summary>
    /// Gets the total price of all items in a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <returns>The total price if the cart was found; otherwise, null.</returns>
    Task<decimal?> GetCartTotalAsync(string cartId);

    /// <summary>
    /// Applies a discount to the cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="discountType">The type of discount (Percentage, FixedAmount).</param>
    /// <param name="discountValue">The value of the discount.</param>
    /// <returns>The updated cart with the applied discount if found; otherwise, null.</returns>
    Task<Cart?> ApplyDiscountAsync(string cartId, string discountType, decimal discountValue);

    /// <summary>
    /// Gets all items from a catalog that can be added to a cart.
    /// </summary>
    /// <returns>A collection of items available for purchase.</returns>
    Task<IEnumerable<Item>> GetAvailableItemsAsync();

    /// <summary>
    /// Gets the items in a cart with their quantities.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <returns>A collection of cart items if the cart was found; otherwise, null.</returns>
    Task<IEnumerable<CartItem>?> GetCartItemsAsync(string cartId);

    /// <summary>
    /// Checks if an item is in stock and available for adding to the cart.
    /// </summary>
    /// <param name="itemId">The ID of the item to check.</param>
    /// <param name="quantity">The quantity to check.</param>
    /// <returns>True if the item is available in the requested quantity; otherwise, false.</returns>
    Task<bool> IsItemAvailableAsync(string itemId, int quantity);
}