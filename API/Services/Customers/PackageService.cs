using API.Implementations.Domain;
using API.Models.Customers;
using API.Models.IntAdmin;
using API.Services.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class PackageService : IPackageService
    {
        private readonly PackageDomain _packageDomain;

        public PackageService(PackageDomain packageDomain)
        {
            _packageDomain = packageDomain;
        }

        public async Task<Result<Package>> CreatePackageAsync(Package package, int customerId)
        {
            return await _packageDomain.CreatePackageAsync(package, customerId);
        }

        public async Task<Result<Package>> GetPackageByIdAsync(string packageId)
        {
            return await _packageDomain.GetPackageByIdAsync(packageId);
        }

        public async Task<Result<List<Package>>> GetPackagesByCustomerIdAsync(int customerId)
        {
            return await _packageDomain.GetPackagesByCustomerIdAsync(customerId);
        }

        public async Task<Result<Package>> UpdatePackageAsync(Package package)
        {
            return await _packageDomain.UpdatePackageAsync(package);
        }

        public async Task<Result<bool>> DeletePackageAsync(string packageId)
        {
            return await _packageDomain.DeletePackageAsync(packageId);
        }

        public async Task<Result<Package>> AddItemToPackageAsync(string packageId, int itemSku, int quantity)
        {
            return await _packageDomain.AddItemToPackageAsync(packageId, itemSku, quantity);
        }

        public async Task<Result<Package>> RemoveItemFromPackageAsync(string packageId, int itemSku)
        {
            return await _packageDomain.RemoveItemFromPackageAsync(packageId, itemSku);
        }

        public async Task<Result<Package>> AddNoteToPackageAsync(string packageId, PackageNote note)
        {
            return await _packageDomain.AddNoteToPackageAsync(packageId, note);
        }

        public async Task<Result<Package>> UpdatePackageStatusAsync(string packageId, string status, string updatedBy, string notes)
        {
            return await _packageDomain.UpdatePackageStatusAsync(packageId, status, updatedBy, notes);
        }

        public async Task<Result<decimal>> CalculateTotalPriceAsync(string packageId)
        {
            return await _packageDomain.CalculateTotalPriceAsync(packageId);
        }

        public async Task<Result<decimal>> CalculateDiscountedPriceAsync(string packageId)
        {
            return await _packageDomain.CalculateDiscountedPriceAsync(packageId);
        }

        public async Task<Result<decimal>> CalculateTotalContractValueAsync(string packageId)
        {
            return await _packageDomain.CalculateTotalContractValueAsync(packageId);
        }

        public async Task<Result<bool>> IsContractActiveAsync(string packageId)
        {
            return await _packageDomain.IsContractActiveAsync(packageId);
        }

        public async Task<Result<TimeSpan>> GetRemainingContractTimeAsync(string packageId)
        {
            return await _packageDomain.GetRemainingContractTimeAsync(packageId);
        }
    }
}