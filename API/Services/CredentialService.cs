using API.implementations.Infrastructure.Data;
using API.Models;

namespace API.Services;

public class CredentialService : ICredentialService
{
    private readonly AppDbContext _context;

    public CredentialService(AppDbContext context) 
    {  
        _context = context; 
    }

    /// <summary>
    /// Creates a new Credential.
    /// </summary>
    /// <param name="credential">The credential to create.</param>
    /// <returns>The created credential.</returns>
    public async Task<Credential> CreateCredentialAsync(Credential credential)
    {
        _context.Credentials.Add(credential);
        await _context.SaveChangesAsync();

        return await Task.FromResult(credential);
    }
}
