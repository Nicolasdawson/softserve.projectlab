using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.implementations.Domain
{
    public class PackageService : IPackageService
    {
        public List<Package> _Packages = new List<Package>();

        private List<Item> _items = new List<Item> // Fuente de datos simulada
        {
            new Item { Sku = "1", Discount = 0 , Price = 20.0m },
            new Item { Sku = "2", Discount = 0, Price = 30.0m },
            new Item { Sku = "3", Discount = 0, Price = 40.0m }
        };


        public async Task<Result<Package>> AddCustomer(string packageId, Customer customer)
        {
            Package? p = _Packages.Find(pak => pak.Id.Equals(packageId));
            if (p != null)
            {
                p.Customer = customer;
                return Result.Ok(p);
            }

            return Result.Fail<Package>("Package not found.");
        }

        public async Task<Result<Package>> AddItem(string packageId, string itemId)
        {
            Package? p = _Packages.Find(pak => pak.Id.Equals(packageId));

            if (p != null)
            {
                Item? itemToAdd = _items.Find(item => item.Sku == itemId);

                if (itemToAdd != null)
                {
                    p.Cart.Add(itemToAdd);
                    return Result.Ok(p);
                }
                return Result.Fail<Package> ("Item not found.");

            }
            return Result.Fail<Package>("Package not found.");    
        }

        public async Task<Result<Package>> CreatePackage(Package package)
        {
            try
            {
                _Packages.Add(package);
                return Result.Ok(package);
            }
            catch (Exception E)
            {
                return Result.Fail<Package>(E.Message);
            }
  
        }

        public async Task<Result<Package>> DeleteItem(string packageId, string itemId)
        {
            Package? p = _Packages.Find(pak => pak.Id.Equals(packageId));

            if (p != null)
            {
                Item? itemToAdd = _items.Find(item => item.Sku == itemId);

                if (itemToAdd != null)
                {
                    p.Cart.Remove(itemToAdd);
                    return Result.Ok(p);
                }

                return Result.Fail<Package>("Item not found.");
            }

            return Result.Fail<Package>("Package not found.");
        }
    }

    // TODO: we want to use a result class so that the controller have the information to know if something in the domain failed
    // https://achraf-chennan.medium.com/using-the-result-class-in-c-519da90351f0
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes
}
