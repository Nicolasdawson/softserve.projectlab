using API.Models;

namespace API.Controllers;

public class RequestProcessor : IRequestProcessor
{
    
    public List<Package> Packages = new List<Package>();
    public Package CreatePackage(Package package)
    {
        Packages.Add(package);
        return package;
    }
    public Package AddItem(string packageId, string itemId)
    {
        Package? p = Packages.Find(delegate(Package pak){
            return pak.Id.Equals(packageId);
        });

        if(p == null){
            p = new Package();
            p.Id = packageId;

            Packages.Add(p);
        }

        Item i = new Item();
        i.Sku = itemId;
        p.Cart.Add(i);

        return p;
    }
    public Package DeleteItem(string packageId, string itemId)
    {
        Package? p = Packages.Find(delegate (Package pak) {
            return pak.Id.Equals(packageId);
        });

        if (p == null)
        {
            p = new Package();
            p.Id = packageId;

            Packages.Add(p);
            return p;
        }

        Item? i = p.Cart.Find(delegate(Item it)
        {
            return it.Sku.Equals(itemId);
        });

        if (i != null)
        {
            p.Cart.Remove(i);
        }

        return p;
    }
    public Package AddCustomer(string packageId, Customer customer)
    {
        Package? p = Packages.Find(delegate (Package pak) {
            return pak.Id.Equals(packageId);
        });

        p.Customer = customer;

        return p;        
    }
}
