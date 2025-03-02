using API.Models;

namespace API.Abstractions
{
    public interface IPackageDomainService
    {
        Task<Package> CreatePackageAsync(Package package);
        Task<Package> AddItemAsync(string packageId, string itemId);
        Task<Package> DeleteItemAsync(string packageId, string itemId);
        Task<Package> AddCustomerAsync(string packageId, Customer customer);
    }
}
