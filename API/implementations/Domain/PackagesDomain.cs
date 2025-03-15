//using API.Abstractions;
using API.Models;

namespace API.implementations.Domain
{
    public class PackagesDomain
    {
        public async Task<Result<Package>> CreatePackage(Package package)
        {
            try
            {
                // Here will add the logic to create a package
                return Result<Package>.Success(package);
            }
            catch (Exception ex)
            {
                return Result<Package>.Failure(ex.Message);
            }
        }

        public async Task<Result<Package>> AddItem(string packageId, Item item)
        {
            try
            {
                // Here will add the logic to add an item to a package
                return Result<Package>.Success(new Package());
            }
            catch (Exception ex)
            {
                return Result<Package>.Failure(ex.Message);
            }
        }

        public async Task<Result<Package>> DeleteItem(string packageId, string itemId)
        {
            try
            {
                // here we will add the logic to delete an item from a package
                return Result<Package>.Success(new Package());
            }
            catch (Exception ex)
            {
                return Result<Package>.Failure(ex.Message);
            }
        }

        public async Task<Result<Package>> AddCustomer(string packageId, Customer customer)
        {
            try
            {
                // here we will add the logic add a customer to a package
                return Result<Package>.Success(new Package());
            }
            catch (Exception ex)
            {
                return Result<Package>.Failure(ex.Message);
            }
        }

        // TODO: we want to use a result class so that the controller have the information to know if something in the domain failed
        // https://achraf-chennan.medium.com/using-the-result-class-in-c-519da90351f0
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes
    }
}