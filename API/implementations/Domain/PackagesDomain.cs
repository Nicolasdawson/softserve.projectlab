using API.Abstractions;
using API.Models;

namespace API.implementations.Domain;

public class PackagesDomain
{
    public async Task<Package> CreatePackage(Package package)
    {
        return package;
    }
    
    public async Task<Package> AddItem(string packageId, Item item)
    {
        return new Package();
    }
    
    public async Task<Package> DeleteItem(string packageId, string itemId)
    {
        return new Package();
    }
    
    public async Task<Package> AddCustomer(string packageId, Customer customer)
    {
        return new Package();
    }
    
    // TODO: we want to use a result class so that the controller have the information to know if something in the domain failed
    // https://achraf-chennan.medium.com/using-the-result-class-in-c-519da90351f0
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes
}