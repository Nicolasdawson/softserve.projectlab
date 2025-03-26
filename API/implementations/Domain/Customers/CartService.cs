using API.Models.Customers;
using API.Models.IntAdmin;
using API.Services.Customers;

namespace API.Implementations.Domain.Customers;

/// <summary>
/// Implementation of the ICartService interface.
/// </summary>
public class CartService : ICartService
{
    private readonly ICustomerService _customerService;
    private readonly IPackageService _packageService;
    
    // In a real application, these would be replaced with database repositories
    private readonly List<Cart> _carts = new();
    private readonly List<Item> _availableItems = new();

    /// <summary>
    /// Initializes a new instance of the CartService class.
    /// </summary>
    /// <param name="customerService">The customer service.</param>
    /// <param name="packageService">The package service.</param>
    public CartService(ICustomerService customerService, IPackageService packageService)
    {
        _customerService = customerService;
        _packageService = packageService;
        
        // Initialize some sample items for testing
        InitializeSampleItems();
    }

    /// <summary>
    /// Gets a cart by ID.
    /// </summary>
    /// <param name="id">The ID of the cart to retrieve.</param>
    /// <returns>The cart if found; otherwise, null.</returns>
    public async Task<Cart?> GetCartByIdAsync(string id)
    {
        await Task.CompletedTask;
        return _carts.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Gets a customer's active cart.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The customer's active cart; creates a new one if none exists.</returns>
    public async Task<Cart> GetOrCreateCartForCustomerAsync(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
        }
        
        var existingCart = _carts.FirstOrDefault(c => c.Customer.Id == customerId);
        if (existingCart != null)
        {
            return existingCart;
        }
        
        // Create a new cart for the customer
        var newCart = new Cart
        {
            Id = Guid.NewGuid().ToString(),
            Customer = customer,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _carts.Add(newCart);
        return newCart;
    }

    /// <summary>
    /// Adds an item to a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="itemId">The ID of the item to add.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <returns>The updated cart if found; otherwise, null.</returns>
    public async Task<Cart?> AddItemToCartAsync(string cartId, string itemId, int quantity)
    {
        var cart = await GetCartByIdAsync(cartId);
        if (cart == null)
        {
            return null;
        }
        
        var item = await GetItemByIdAsync(itemId);
        if (item == null)
        {
            throw new KeyNotFoundException($"Item with ID {itemId} not found.");
        }
        
        if (!await IsItemAvailableAsync(itemId, quantity))
        {
            throw new InvalidOperationException($"Item with ID {itemId} is not available in the requested quantity.");
        }
        
        cart.AddItem(item, quantity);
        return cart;
    }

    /// <summary>
    /// Updates the quantity of an item in a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="itemId">The ID of the item to update.</param>
    /// <param name="quantity">The new quantity.</param>
    /// <returns>The updated cart if found; otherwise, null.</returns>
    public async Task<Cart?> UpdateCartItemQuantityAsync(string cartId, string itemId, int quantity)
    {
        var cart = await GetCartByIdAsync(cartId);
        if (cart == null)
        {
            return null;
        }
        
        if (quantity <= 0)
        {
            // Remove the item if quantity is 0 or negative
            return await RemoveItemFromCartAsync(cartId, itemId);
        }
        
        var existingItem = cart.Items.FirstOrDefault(i => i.Item.Id == itemId);
        if (existingItem == null)
        {
            // Item doesn't exist in the cart, add it
            return await AddItemToCartAsync(cartId, itemId, quantity);
        }
        
        // Check if the new quantity is available
        if (!await IsItemAvailableAsync(itemId, quantity))
        {
            throw new InvalidOperationException($"Item with ID {itemId} is not available in the requested quantity.");
        }
        
        // Remove the item and add it again with the new quantity
        cart.RemoveItem(itemId, existingItem.Quantity);
        cart.AddItem(existingItem.Item, quantity);
        
        return cart;
    }

    /// <summary>
    /// Removes an item from a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="itemId">The ID of the item to remove.</param>
    /// <returns>The updated cart if found; otherwise, null.</returns>
    public async Task<Cart?> RemoveItemFromCartAsync(string cartId, string itemId)
    {
        var cart = await GetCartByIdAsync(cartId);
        if (cart == null)
        {
            return null;
        }
        
        var existingItem = cart.Items.FirstOrDefault(i => i.Item.Id == itemId);
        if (existingItem == null)
        {
            // Item doesn't exist in the cart, nothing to remove
            return cart;
        }
        
        cart.RemoveItem(itemId, existingItem.Quantity);
        return cart;
    }

    /// <summary>
    /// Clears all items from a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart to clear.</param>
    /// <returns>The cleared cart if found; otherwise, null.</returns>
    public async Task<Cart?> ClearCartAsync(string cartId)
    {
        var cart = await GetCartByIdAsync(cartId);
        if (cart == null)
        {
            return null;
        }
        
        cart.Clear();
        return cart;
    }

    /// <summary>
    /// Converts a cart to a package.
    /// </summary>
    /// <param name="cartId">The ID of the cart to convert.</param>
    /// <param name="packageName">The name for the new package.</param>
    /// <param name="contractTermMonths">The contract term in months.</param>
    /// <returns>The created package if the cart was found; otherwise, null.</returns>
    public async Task<Package?> ConvertCartToPackageAsync(string cartId, string packageName, int contractTermMonths)
    {
        var cart = await GetCartByIdAsync(cartId);
        if (cart == null || cart.Items.Count == 0)
        {
            return null;
        }
        
        var package = new Package
        {
            Id = Guid.NewGuid().ToString(),
            Name = packageName,
            Customer = cart.Customer,
            SaleDate = DateTime.UtcNow,
            ContractTermMonths = contractTermMonths,
            ContractStartDate = DateTime.UtcNow,
            Status = "Processing"
        };
        
        // Add items from the cart to the package
        foreach (var cartItem in cart.Items)
        {
            package.AddItem(cartItem.Item, cartItem.Quantity);
        }
        
        // Clear the cart after converting to a package
        await ClearCartAsync(cartId);
        
        // Create the package
        return await _packageService.CreatePackageAsync(package);
    }

    /// <summary>
    /// Gets the total price of all items in a cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <returns>The total price if the cart was found; otherwise, null.</returns>
    public async Task<decimal?> GetCartTotalAsync(string cartId)
    {
        var cart = await GetCartByIdAsync(cartId);
        return cart?.GetTotalPrice();
    }

    /// <summary>
    /// Applies a discount to the cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <param name="discountType">The type of discount (Percentage, FixedAmount).</param>
    /// <param name="discountValue">The value of the discount.</param>
    /// <returns>The updated cart with the applied discount if found; otherwise, null.</returns>
    public async Task<Cart?> ApplyDiscountAsync(string cartId, string discountType, decimal discountValue)
    {
        // This method would typically apply a discount to the cart
        // For simplicity, we'll just return the cart without implementing the discount logic
        return await GetCartByIdAsync(cartId);
    }

    /// <summary>
    /// Gets all items from a catalog that can be added to a cart.
    /// </summary>
    /// <returns>A collection of items available for purchase.</returns>
    public async Task<IEnumerable<Item>> GetAvailableItemsAsync()
    {
        await Task.CompletedTask;
        return _availableItems;
    }

    /// <summary>
    /// Gets the items in a cart with their quantities.
    /// </summary>
    /// <param name="cartId">The ID of the cart.</param>
    /// <returns>A collection of cart items if the cart was found; otherwise, null.</returns>
    public async Task<IEnumerable<CartItem>?> GetCartItemsAsync(string cartId)
    {
        var cart = await GetCartByIdAsync(cartId);
        return cart?.Items;
    }

    /// <summary>
    /// Checks if an item is in stock and available for adding to the cart.
    /// </summary>
    /// <param name="itemId">The ID of the item to check.</param>
    /// <param name="quantity">The quantity to check.</param>
    /// <returns>True if the item is available in the requested quantity; otherwise, false.</returns>
    public async Task<bool> IsItemAvailableAsync(string itemId, int quantity)
    {
        await Task.CompletedTask;
        
        // In a real application, this would check the inventory
        // For simplicity, we'll assume all items are available in any quantity
        var item = _availableItems.FirstOrDefault(i => i.Id == itemId);
        return item != null;
    }

    /// <summary>
    /// Gets an item by ID.
    /// </summary>
    /// <param name="itemId">The ID of the item to retrieve.</param>
    /// <returns>The item if found; otherwise, null.</returns>
    private async Task<Item?> GetItemByIdAsync(string itemId)
    {
        await Task.CompletedTask;
        return _availableItems.FirstOrDefault(i => i.Id == itemId);
    }

    /// <summary>
    /// Initializes some sample items for testing.
    /// </summary>
    private void InitializeSampleItems()
    {
        _availableItems.Add(new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Security Camera",
            Description = "High-definition security camera with night vision",
            Price = 199.99m
        });
        
        _availableItems.Add(new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Smart Doorbell",
            Description = "Video doorbell with two-way audio and motion detection",
            Price = 149.99m
        });
        
        _availableItems.Add(new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Security Drone",
            Description = "Autonomous security drone with 4K camera and facial recognition",
            Price = 999.99m
        });
        
        _availableItems.Add(new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Smart Home Hub",
            Description = "Central hub for controlling all your smart home devices",
            Price = 249.99m
        });
        
        _availableItems.Add(new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Motion Sensor",
            Description = "Wireless motion sensor with smartphone alerts",
            Price = 49.99m
        });
    }
}