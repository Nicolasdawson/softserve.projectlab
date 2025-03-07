using API.Models;

namespace API.Services.Interfaces
{
    public interface IPackageService
    {
        Task<Package> CreatePackageAsync(Package package);
        Task<Package> AddItemAsync(string packageId, string itemId);
        Task<Package> DeleteItemAsync(string packageId, string itemId);
        Task<Package> AddCustomerAsync(string packageId, Customer customer);
    }
}
