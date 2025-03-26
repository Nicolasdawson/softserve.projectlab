using API.Models.Customers;
using API.Models.IntAdmin;
using API.Services.Customers;

namespace API.Implementations.Domain.Customers;

/// <summary>
/// Implementation of the IPackageService interface.
/// </summary>
public class PackageService : IPackageService
{
    private readonly ICustomerService _customerService;
    
    // In a real application, these would be replaced with database repositories
    private readonly List<Package> _packages = new();
    private readonly List<Item> _availableItems;

    /// <summary>
    /// Initializes a new instance of the PackageService class.
    /// </summary>
    /// <param name="customerService">The customer service.</param>
    public PackageService(ICustomerService customerService)
    {
        _customerService = customerService;
        
        // Initialize the available items list
        // In a real application, this would come from a catalog service or repository
        _availableItems = new List<Item>
        {
            new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Security Camera",
                Description = "High-definition security camera with night vision",
                Price = 199.99m
            },
            new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Smart Doorbell",
                Description = "Video doorbell with two-way audio and motion detection",
                Price = 149.99m
            },
            new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Security Drone",
                Description = "Autonomous security drone with 4K camera and facial recognition",
                Price = 999.99m
            }
        };
    }

    /// <summary>
    /// Gets all packages.
    /// </summary>
    /// <returns>A collection of all packages.</returns>
    public async Task<IEnumerable<Package>> GetAllPackagesAsync()
    {
        await Task.CompletedTask;
        return _packages;
    }

    /// <summary>
    /// Gets a package by ID.
    /// </summary>
    /// <param name="id">The ID of the package to retrieve.</param>
    /// <returns>The package if found; otherwise, null.</returns>
    public async Task<Package?> GetPackageByIdAsync(string id)
    {
        await Task.CompletedTask;
        return _packages.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Gets packages for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>A collection of packages for the customer.</returns>
    public async Task<IEnumerable<Package>> GetPackagesByCustomerIdAsync(string customerId)
    {
        await Task.CompletedTask;
        return _packages.Where(p => p.Customer.Id == customerId);
    }

    /// <summary>
    /// Gets packages by status.
    /// </summary>
    /// <param name="status">The status of packages to retrieve.</param>
    /// <returns>A collection of packages with the specified status.</returns>
    public async Task<IEnumerable<Package>> GetPackagesByStatusAsync(string status)
    {
        await Task.CompletedTask;
        return _packages.Where(p => p.Status == status);
    }

    /// <summary>
    /// Creates a new package.
    /// </summary>
    /// <param name="package">The package to create.</param>
    /// <returns>The created package.</returns>
    public async Task<Package> CreatePackageAsync(Package package)
    {
        await Task.CompletedTask;
        
        // Ensure the package has an ID
        if (string.IsNullOrEmpty(package.Id))
        {
            package.Id = Guid.NewGuid().ToString();
        }
        
        _packages.Add(package);
        return package;
    }

    /// <summary>
    /// Updates an existing package.
    /// </summary>
    /// <param name="id">The ID of the package to update.</param>
    /// <param name="package">The updated package information.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> UpdatePackageAsync(string id, Package package)
    {
        var existingPackage = await GetPackageByIdAsync(id);
        if (existingPackage == null)
        {
            return null;
        }
        
        // Preserve the ID
        package.Id = id;
        
        // Update the package
        _packages.Remove(existingPackage);
        _packages.Add(package);
        
        return package;
    }

    /// <summary>
    /// Updates the status of a package.
    /// </summary>
    /// <param name="id">The ID of the package to update.</param>
    /// <param name="status">The new status.</param>
    /// <param name="updatedBy">The user updating the status.</param>
    /// <param name="notes">Optional notes about the status update.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> UpdatePackageStatusAsync(string id, string status, string updatedBy, string? notes = null)
    {
        var package = await GetPackageByIdAsync(id);
        if (package == null)
        {
            return null;
        }
        
        package.UpdateStatus(status, updatedBy, notes);
        return package;
    }

    /// <summary>
    /// Adds an item to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="itemId">The ID of the item to add.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> AddItemToPackageAsync(string packageId, string itemId, int quantity)
    {
        var package = await GetPackageByIdAsync(packageId);
        if (package == null)
        {
            return null;
        }
        
        var item = await GetItemByIdAsync(itemId);
        if (item == null)
        {
            throw new KeyNotFoundException($"Item with ID {itemId} not found.");
        }
        
        package.AddItem(item, quantity);
        return package;
    }

    /// <summary>
    /// Removes an item from a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="itemId">The ID of the item to remove.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> RemoveItemFromPackageAsync(string packageId, string itemId)
    {
        var package = await GetPackageByIdAsync(packageId);
        if (package == null)
        {
            return null;
        }
        
        var existingItem = package.Items.FirstOrDefault(i => i.Item.Id == itemId);
        if (existingItem == null)
        {
            // Item doesn't exist in the package, nothing to remove
            return package;
        }
        
        package.Items.Remove(existingItem);
        return package;
    }

    /// <summary>
    /// Gets the total price of a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <returns>The total price if the package was found; otherwise, null.</returns>
    public async Task<decimal?> GetPackageTotalAsync(string packageId)
    {
        var package = await GetPackageByIdAsync(packageId);
        return package?.CalculateTotalPrice();
    }

    /// <summary>
    /// Gets the total contract value of a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <returns>The total contract value if the package was found; otherwise, null.</returns>
    public async Task<decimal?> GetContractValueAsync(string packageId)
    {
        var package = await GetPackageByIdAsync(packageId);
        return package?.CalculateTotalContractValue();
    }

    /// <summary>
    /// Renews a package.
    /// </summary>
    /// <param name="packageId">The ID of the package to renew.</param>
    /// <param name="renewalTermMonths">The renewal term in months.</param>
    /// <returns>The newly created renewal package if the original package was found; otherwise, null.</returns>
    public async Task<Package?> RenewPackageAsync(string packageId, int renewalTermMonths)
    {
        var originalPackage = await GetPackageByIdAsync(packageId);
        if (originalPackage == null)
        {
            return null;
        }
        
        // Create a new package based on the original package
        var renewalPackage = new Package
        {
            Id = Guid.NewGuid().ToString(),
            Name = originalPackage.Name,
            Description = originalPackage.Description,
            Customer = originalPackage.Customer,
            SaleDate = DateTime.UtcNow,
            ContractTermMonths = renewalTermMonths,
            ContractStartDate = originalPackage.ContractEndDate, // Start when the previous contract ends
            MonthlyFee = originalPackage.MonthlyFee,
            SetupFee = 0, // No setup fee for renewals
            IsRenewal = true,
            PreviousPackageId = originalPackage.Id
        };
        
        // Copy items from the original package
        foreach (var item in originalPackage.Items)
        {
            renewalPackage.AddItem(item.Item, item.Quantity);
        }
        
        // Add a note about the renewal
        renewalPackage.AddNote(
            "Renewal",
            $"This package is a renewal of package {originalPackage.Id} with a term of {renewalTermMonths} months.",
            "System"
        );
        
        // Create the renewal package
        return await CreatePackageAsync(renewalPackage);
    }

    /// <summary>
    /// Cancels a package.
    /// </summary>
    /// <param name="packageId">The ID of the package to cancel.</param>
    /// <param name="canceledBy">The user canceling the package.</param>
    /// <param name="cancellationReason">The reason for cancellation.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> CancelPackageAsync(string packageId, string canceledBy, string cancellationReason)
    {
        var package = await GetPackageByIdAsync(packageId);
        if (package == null)
        {
            return null;
        }
        
        package.UpdateStatus("Canceled", canceledBy, cancellationReason);
        return package;
    }

    /// <summary>
    /// Adds a note to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="title">The title of the note.</param>
    /// <param name="content">The content of the note.</param>
    /// <param name="createdBy">The user creating the note.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> AddNoteToPackageAsync(string packageId, string title, string content, string createdBy)
    {
        var package = await GetPackageByIdAsync(packageId);
        if (package == null)
        {
            return null;
        }
        
        package.AddNote(title, content, createdBy);
        return package;
    }

    /// <summary>
    /// Gets the notes for a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <returns>A collection of package notes if the package was found; otherwise, null.</returns>
    public async Task<IEnumerable<PackageNote>?> GetPackageNotesAsync(string packageId)
    {
        var package = await GetPackageByIdAsync(packageId);
        return package?.Notes;
    }

    /// <summary>
    /// Applies a discount to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="discountAmount">The discount amount to apply.</param>
    /// <param name="reason">The reason for the discount.</param>
    /// <param name="appliedBy">The user applying the discount.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    public async Task<Package?> ApplyDiscountAsync(string packageId, decimal discountAmount, string reason, string appliedBy)
    {
        var package = await GetPackageByIdAsync(packageId);
        if (package == null)
        {
            return null;
        }
        
        // Apply the discount
        package.DiscountAmount = discountAmount;
        
        // Add a note about the discount
        package.AddNote(
            "Discount Applied",
            $"Discount of ${discountAmount} applied to package. Reason: {reason}",
            appliedBy
        );
        
        return package;
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
}