using API.Models;

namespace API.Services;

public class PackageService
{
    private readonly List<Package> _packages = new();

    public Package CreatePackage(Package package)
    {
        _packages.Add(package);
        return package;
    }

    public Package AddItemToPackage(string packageId, Item item)
    {
        var package = _packages.FirstOrDefault(p => p.Id == packageId);
        if (package == null) throw new Exception("Package not found");

        package.Cart.Add(item);
        return package;
    }

    public Package RemoveItemFromPackage(string packageId, string itemSku)
    {
        var package = _packages.FirstOrDefault(p => p.Id == packageId);
        if (package == null) throw new Exception("Package not found");

        var item = package.Cart.FirstOrDefault(i => i.Sku == itemSku);
        if (item == null) throw new Exception("Item not found in package");

        package.Cart.Remove(item);
        return package;
    }

    public Package AddCustomerToPackage(string packageId, Customer customer)
    {
        var package = _packages.FirstOrDefault(p => p.Id == packageId);
        if (package == null) throw new Exception("Package not found");

        package.Customer = customer;
        return package;
    }
}
