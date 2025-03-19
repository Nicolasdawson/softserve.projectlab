using API.Models;

namespace API.implementations.Domain
{
    public interface IPackageService
    {
        Task<Package> CreatePackage(Package package);
        Task<Package> AddItem(string packageId, string itemId);
        Task<Package> DeleteItem(string packageId, string itemId);
        Task<Package> AddCustomer(string packageId, Customer customer);
    }
}
