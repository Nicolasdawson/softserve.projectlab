using API.Models;

namespace API.implementations.Domain
{
    public interface IPackageService
    {
        Task<Result<Package>> CreatePackage(Package package);
        Task<Result<Package>> AddItem(string packageId, string itemId);
        Task<Result<Package>> DeleteItem(string packageId, string itemId);
        Task<Result<Package>> AddCustomer(string packageId, Customer customer);
    }
}
