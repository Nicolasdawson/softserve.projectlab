namespace API.Services;

using API.Models;

public class PackageService
{
    private readonly List<Package> _packages = new();

    public Package CreatePackage(Package package)
    {
        _packages.Add(package);
        return package;
    }

    public Package AddItemToPackage(string packageId, string itemId)
    {
        var package = _packages.FirstOrDefault(p => p.Id == packageId);
        if (package == null) throw new Exception("Package not found");

        package.Items.Add(itemId);
        return package;
    }

    public Package RemoveItemFromPackage(string packageId, string itemId)
    {
        var package = _packages.FirstOrDefault(p => p.Id == packageId);
        if (package == null) throw new Exception("Package not found");

        package.Items.Remove(itemId);
        return package;
    }

    public Package AddCustomerToPackage(string packageId, Customer customer)
    {
        var package = _packages.FirstOrDefault(p => p.Id == packageId);
        if (package == null) throw new Exception("Package not found");

        package.Customers.Add(customer);
        return package;
    }
}
