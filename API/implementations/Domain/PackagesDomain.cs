
using API.Data.Entities;
using API.Models;
using API.Models.Customers;
using API.Models.IntAdmin;

namespace API.implementations.Domain
{
    public class PackagesDomain
    {
        /// <summary>
        /// Creates a new package.
        /// </summary>
        /// <param name="package">The package to create.</param>
        /// <returns>A result containing the created package or an error message.</returns>
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

        /// <summary>
        /// Adds an item to an existing package.
        /// </summary>
        /// <param name="packageId">The ID of the package.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>A result containing the updated package or an error message.</returns>
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

        /// <summary>
        /// Deletes an item from an existing package.
        /// </summary>
        /// <param name="packageId">The ID of the package.</param>
        /// <param name="itemId">The ID of the item to delete.</param>
        /// <returns>A result containing the updated package or an error message.</returns>
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

        /// <summary>
        /// Adds a customer to an existing package.
        /// </summary>
        /// <param name="packageId">The ID of the package.</param>
        /// <param name="customer">The customer to add.</param>
        /// <returns>A result containing the updated package or an error message.</returns>
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