using API.Models;

namespace API.implementations.Domain
{
    public class PackageService : IPackageService
    {
        public List<Package> _Packages = new List<Package>();
        

        public Task<Package> AddCustomer(string packageId, Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Package> AddItem(string packageId, string itemId)
        {
            Package? p = _Packages.Find(pak => pak.Id.Equals(packageId));

            if (p == null)
            {
                throw new Exception("Package not found");
            }



            throw new NotImplementedException();
        }

        public async Task<Package> CreatePackage(Package package)
        {
            _Packages.Add(package);
            return package;
        }

        public Task<Package> DeleteItem(string packageId, string itemId)
        {
            throw new NotImplementedException();
        }
    }
}
