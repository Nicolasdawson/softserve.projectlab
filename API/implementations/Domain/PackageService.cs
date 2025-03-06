using API.Abstractions;
using API.Models;

public class PackageService : IPackageService
{
    private readonly List<Package> _packages = new();

    public async Task<Package> CreatePackage(Package package)
    {
        _packages.Add(package);
        return await Task.FromResult(package);
    }

    public async Task<Package> AddItemToPackage(string packageId, string itemId)
    {
        // Lógica para añadir el item
        return await Task.FromResult(new Package());
    }

    public async Task<Package> RemoveItemFromPackage(string packageId, string itemId)
    {
        // Lógica para eliminar el item
        return await Task.FromResult(new Package());
    }

    public async Task<Package> AddCustomerToPackage(string packageId, Customer customer)
    {
        // Lógica para añadir el cliente
        return await Task.FromResult(new Package());
    }
}
