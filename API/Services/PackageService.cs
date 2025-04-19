using API.Models;

namespace API.Services
{
    public class PackageService
    {
        private readonly List<Package> _packages = new();

        public async Task<Package> CreatePackage(Package package)
        {
            _packages.Add(package);
            return package;
        }

        public async Task<Package> AddItemToPackage(string packageId, string itemSku)
        {
            var package = _packages.FirstOrDefault(p => p.Id == packageId);
            if (package == null) return null;

            package.Cart.Add(new Item { Sku = itemSku });
            return package;
        }

        public async Task<bool> DeleteItemFromPackage(string packageId, string itemSku)
        {
            var package = _packages.FirstOrDefault(p => p.Id == packageId);
            if (package == null) return false;

            var item = package.Cart.FirstOrDefault(i => i.Sku == itemSku);
            if (item == null) return false;

            package.Cart.Remove(item);
            return true;
        }

        public async Task<Package> AddCustomerToPackage(string packageId, Customer customer)
        {
            var package = _packages.FirstOrDefault(p => p.Id == packageId);
            if (package == null) return null;

            package.Customer = customer;
            return package;
        }
    }
}