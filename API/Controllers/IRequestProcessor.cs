using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public interface IRequestProcessor
{

    public Package CreatePackage(Package package);
    public Package AddItem(string packageId, string itemId);
    public Package DeleteItem(string packageId, string itemId);
    public Package AddCustomer(string packageId, Customer customer);
}
