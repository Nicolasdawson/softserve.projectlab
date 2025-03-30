using API.Data.Entities;
using API.Models.Customers;
using API.Models.IntAdmin;

namespace API.Services
{
    public interface IPackageService
    {
        Task<Result<Package>> CreatePackageAsync(Package package);
        Task<Result<Package>> AddItemAsync(string packageId, Item item);
        Task<Result<Package>> DeleteItemAsync(string packageId, string itemId);
        Task<Result<Package>> AddCustomerAsync(string packageId, Customer customer);
    }
}