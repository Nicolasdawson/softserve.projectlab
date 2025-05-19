using API.Models;

namespace API.Services;

public interface ICredentialService
{
    Task<Credential> CreateCredentialAsync(Credential credential);
}
