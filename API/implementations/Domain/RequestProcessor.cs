//using API.Models;

namespace API.implementations.Domain;

public class RequestProcessor// : IRequestProcessor
{
    
    //public List<Package> Packages = new List<Package>();
    //public async Task<Result<Package>> CreatePackage(Package package)
    //{
    //    try
    //    {
    //        Packages.Add(package);
    //        return Result.Ok(package);
    //    } catch (Exception E)
    //    {
    //        return Result.Fail<Package>(E.Message);
    //    }

    //}
    //public async Task<Result<Package>> AddItem(string packageId, string itemId)
    //{
    //    try
    //    {
    //        Package? p = Packages.Find(delegate (Package pak) {
    //            return pak.Id.Equals(packageId);
    //        });

    //        if (p == null)
    //        {
    //            p = new Package();
    //            p.Id = packageId;

    //            Packages.Add(p);
    //        }

    //        Item i = new Item();
    //        i.Sku = itemId;
    //        p.Cart.Add(i);

    //        return Result.Ok(p);
    //    }
    //    catch (Exception E)
    //    {
    //        return Result.Fail<Package>(E.Message);
    //    }

    //}
    //public async Task<Result<Package>> DeleteItem(string packageId, string itemId)
    //{

    //    try
    //    {
    //        Package? p = Packages.Find(delegate (Package pak) {
    //            return pak.Id.Equals(packageId);
    //        });

    //        if (p == null)
    //        {
    //            p = new Package();
    //            p.Id = packageId;

    //            Packages.Add(p);
    //            return Result.Ok(p);
    //        }

    //        Item? i = p.Cart.Find(delegate (Item it)
    //        {
    //            return it.Sku.Equals(itemId);
    //        });

    //        if (i != null)
    //        {
    //            p.Cart.Remove(i);
    //        }

    //        return Result.Ok(p);
    //    }
    //    catch (Exception E)
    //    {
    //        return Result.Fail<Package>(E.Message);
    //    }

    //}
    //public async Task<Result<Package>> AddCustomer(string packageId, Customer customer)
    //{
    //    try
    //    {
    //        Package? p = Packages.Find(delegate (Package pak) {
    //            return pak.Id.Equals(packageId);
    //        });

    //        p.Customer = customer;

    //        return Result.Ok(p);
    //    }
    //    catch (Exception E)
    //    {
    //        return Result.Fail<Package>(E.Message);
    //    }
    //}
}
