using API.Models.Customers;

namespace API.Services.Customers;

/// <summary>
/// Provides operations for managing packages.
/// </summary>
public interface IPackageService
{
    /// <summary>
    /// Gets all packages.
    /// </summary>
    /// <returns>A collection of all packages.</returns>
    Task<IEnumerable<Package>> GetAllPackagesAsync();

    /// <summary>
    /// Gets a package by ID.
    /// </summary>
    /// <param name="id">The ID of the package to retrieve.</param>
    /// <returns>The package if found; otherwise, null.</returns>
    Task<Package?> GetPackageByIdAsync(string id);

    /// <summary>
    /// Gets packages for a customer.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>A collection of packages for the customer.</returns>
    Task<IEnumerable<Package>> GetPackagesByCustomerIdAsync(string customerId);

    /// <summary>
    /// Gets packages by status.
    /// </summary>
    /// <param name="status">The status of packages to retrieve.</param>
    /// <returns>A collection of packages with the specified status.</returns>
    Task<IEnumerable<Package>> GetPackagesByStatusAsync(string status);

    /// <summary>
    /// Creates a new package.
    /// </summary>
    /// <param name="package">The package to create.</param>
    /// <returns>The created package.</returns>
    Task<Package> CreatePackageAsync(Package package);

    /// <summary>
    /// Updates an existing package.
    /// </summary>
    /// <param name="id">The ID of the package to update.</param>
    /// <param name="package">The updated package information.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> UpdatePackageAsync(string id, Package package);

    /// <summary>
    /// Updates the status of a package.
    /// </summary>
    /// <param name="id">The ID of the package to update.</param>
    /// <param name="status">The new status.</param>
    /// <param name="updatedBy">The user updating the status.</param>
    /// <param name="notes">Optional notes about the status update.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> UpdatePackageStatusAsync(string id, string status, string updatedBy, string? notes = null);

    /// <summary>
    /// Adds an item to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="itemId">The ID of the item to add.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> AddItemToPackageAsync(string packageId, string itemId, int quantity);

    /// <summary>
    /// Removes an item from a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="itemId">The ID of the item to remove.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> RemoveItemFromPackageAsync(string packageId, string itemId);

    /// <summary>
    /// Gets the total price of a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <returns>The total price if the package was found; otherwise, null.</returns>
    Task<decimal?> GetPackageTotalAsync(string packageId);

    /// <summary>
    /// Gets the total contract value of a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <returns>The total contract value if the package was found; otherwise, null.</returns>
    Task<decimal?> GetContractValueAsync(string packageId);

    /// <summary>
    /// Renews a package.
    /// </summary>
    /// <param name="packageId">The ID of the package to renew.</param>
    /// <param name="renewalTermMonths">The renewal term in months.</param>
    /// <returns>The newly created renewal package if the original package was found; otherwise, null.</returns>
    Task<Package?> RenewPackageAsync(string packageId, int renewalTermMonths);

    /// <summary>
    /// Cancels a package.
    /// </summary>
    /// <param name="packageId">The ID of the package to cancel.</param>
    /// <param name="canceledBy">The user canceling the package.</param>
    /// <param name="cancellationReason">The reason for cancellation.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> CancelPackageAsync(string packageId, string canceledBy, string cancellationReason);

    /// <summary>
    /// Adds a note to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="title">The title of the note.</param>
    /// <param name="content">The content of the note.</param>
    /// <param name="createdBy">The user creating the note.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> AddNoteToPackageAsync(string packageId, string title, string content, string createdBy);

    /// <summary>
    /// Gets the notes for a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <returns>A collection of package notes if the package was found; otherwise, null.</returns>
    Task<IEnumerable<PackageNote>?> GetPackageNotesAsync(string packageId);

    /// <summary>
    /// Applies a discount to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="discountAmount">The discount amount to apply.</param>
    /// <param name="reason">The reason for the discount.</param>
    /// <param name="appliedBy">The user applying the discount.</param>
    /// <returns>The updated package if found; otherwise, null.</returns>
    Task<Package?> ApplyDiscountAsync(string packageId, decimal discountAmount, string reason, string appliedBy);
}