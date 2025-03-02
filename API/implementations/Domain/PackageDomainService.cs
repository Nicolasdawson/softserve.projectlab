using API.Abstractions;
using API.Models;

namespace API.Implementations.Domain
{
    public class PackageDomainService : IPackageDomainService
    {
        public Task<Package> CreatePackageAsync(Package package)
        {
            return Task.FromResult(package);
        }

        public Task<Package> AddItemAsync(string packageId, string itemId)
        {
            return Task.FromResult(new Package());
        }

        public Task<Package> DeleteItemAsync(string packageId, string itemId)
        {
            return Task.FromResult(new Package());
        }

        public Task<Package> AddCustomerAsync(string packageId, Customer customer)
        {
            return Task.FromResult(new Package());
        }
    }
}
