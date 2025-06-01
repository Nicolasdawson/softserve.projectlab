using API.Models;

namespace API.Services;

public interface ICredentialService
{
    Task<Credential> CreateCredentialAsync(Credential credential);
    Task<Credential?> GetByIdCustomerAsync(int id);
    Task<Credential?> UpdateCredentialAsync(Credential updatedCredential);

    Task<Credential?> GetByRefreshTokenAsync(string refreshToken);
}
