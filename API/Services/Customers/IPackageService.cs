using API.Models.Customers;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IPackageService
    {
        Task<Result<Package>> CreatePackageAsync(Package package, int customerId);
        Task<Result<Package>> GetPackageByIdAsync(string packageId);
        Task<Result<List<Package>>> GetPackagesByCustomerIdAsync(int customerId);
        Task<Result<Package>> UpdatePackageAsync(Package package);
        Task<Result<bool>> DeletePackageAsync(string packageId);
        Task<Result<Package>> AddItemToPackageAsync(string packageId, int itemSku, int quantity);
        Task<Result<Package>> RemoveItemFromPackageAsync(string packageId, int itemSku);
        Task<Result<Package>> AddNoteToPackageAsync(string packageId, PackageNote note);
        Task<Result<Package>> UpdatePackageStatusAsync(string packageId, string status, string updatedBy, string notes);
        Task<Result<decimal>> CalculateTotalPriceAsync(string packageId);
        Task<Result<decimal>> CalculateDiscountedPriceAsync(string packageId);
        Task<Result<decimal>> CalculateTotalContractValueAsync(string packageId);
        Task<Result<bool>> IsContractActiveAsync(string packageId);
        Task<Result<TimeSpan>> GetRemainingContractTimeAsync(string packageId);
    }
}