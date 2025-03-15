using API.Abstractions;
using API.Models;

namespace API.Implementations.Domain
{
    public class PackagesDomain
    {
        private readonly IPackageRepository _packageRepository;

        public PackagesDomain(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Result<Package>> CreatePackage(Package package)
        {
            if (package == null)
            {
                return Result<Package>.Failure("Paquete no puede ser nulo.");
            }

            var createdPackage = await _packageRepository.CreatePackageAsync(package);
            return Result<Package>.Success(createdPackage);
        }

        public async Task<Result<Package>> AddItem(string packageId, string itemId)
        {
            if (string.IsNullOrEmpty(packageId) || string.IsNullOrEmpty(itemId))
            {
                return Result<Package>.Failure("ID de paquete o item inválido.");
            }

            var updatedPackage = await _packageRepository.AddItemAsync(packageId, itemId);
            return Result<Package>.Success(updatedPackage);
        }

        public async Task<Result<Package>> DeleteItem(string packageId, string itemId)
        {
            if (string.IsNullOrEmpty(packageId) || string.IsNullOrEmpty(itemId))
            {
                return Result<Package>.Failure("ID de paquete o item inválidos.");
            }

            var updatedPackage = await _packageRepository.DeleteItemAsync(packageId, itemId);
            return Result<Package>.Success(updatedPackage);
        }

        public async Task<Result<Package>> AddCustomer(string packageId, Customer customer)
        {
            if (string.IsNullOrEmpty(packageId) || customer == null)
            {
                return Result<Package>.Failure("ID de paquete o cliente inválido.");
            }

            var updatedPackage = await _packageRepository.AddCustomerAsync(packageId, customer);
            return Result<Package>.Success(updatedPackage);
        }
    }
}

    // TODO: we want to use a result class so that the controller have the information to know if something in the domain failed
    // https://achraf-chennan.medium.com/using-the-result-class-in-c-519da90351f0
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes
