using API.Abstractions;
using API.Models;

namespace API.Implementations.Infrastructure
{
    public class PackageRepository : IPackageRepository
    {
        private readonly List<Package> _packages = new List<Package>();
        private readonly List<Item> _items = new List<Item>();
        private readonly List<Customer> _customers = new List<Customer>();

        public async Task<Package> CreatePackageAsync(Package package)
        {
            _packages.Add(package);
            return await Task.FromResult(package);
        }

        public async Task<Package> AddItemAsync(string packageId, string sku)
        {
            var package = _packages.FirstOrDefault(p => p.Id == packageId);
            var item = _items.FirstOrDefault(i => i.Sku == sku);
            if (package != null && item != null)
            {
                package.Cart.Add(item);
            }
            return await Task.FromResult(package);
        }

        public async Task<Package> DeleteItemAsync(string packageId, string sku)
        {
            var package = _packages.FirstOrDefault(p => p.Id == packageId);
            var item = package?.Cart.FirstOrDefault(i => i.Sku == sku);
            if (item != null)
            {
                package.Cart.Remove(item);
            }
            return await Task.FromResult(package);
        }

        public async Task<Package> AddCustomerAsync(string packageId, Customer customer)
        {
            var package = _packages.FirstOrDefault(p => p.Id == packageId);
            if (package != null)
            {
                package.Customer = customer;
            }
            return await Task.FromResult(package);
        }
    }
}
