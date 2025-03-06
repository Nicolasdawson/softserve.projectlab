using API.Models;

namespace API.Abstractions
{
    public interface IPackageService
    {
        Task<Package> CreatePackage(Package package);
        Task<Package> AddItemToPackage(string packageId, string itemId);
        Task<Package> RemoveItemFromPackage(string packageId, string itemId);
        Task<Package> AddCustomerToPackage(string packageId, Customer customer);
    }
}
