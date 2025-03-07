using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public interface IRequestProcessor
{

    public Task<Package> CreatePackage(Package package);
    public Task<Package> AddItem(string packageId, string itemId);
    public Task<Package> DeleteItem(string packageId, string itemId);
    public Task<Package> AddCustomer(string packageId, Customer customer);
}
