using API.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class PackageService : IPackageService
    {
        public async Task<Package> CreatePackageAsync(Package package)
        {
            //Loigc to create a package
            return await Task.FromResult(package);
        }

        public async Task<Package> AddItemAsync(string packageId, string itemId)
        {
            //Logic to add an item to a package
            return await Task.FromResult(new Package());
        }

        public async Task<Package> DeleteItemAsync(string packageId, string itemId)
        {
            //Logic to delete an item from a package
            return await Task.FromResult(new Package());
        }

        public async Task<Package> AddCustomerAsync(string packageId, Customer customer)
        {
            //Logic to add a customer
            return await Task.FromResult(new Package());
        }
    }
}
