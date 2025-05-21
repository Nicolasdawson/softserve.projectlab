using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Credential?> GetByIdCustomerAsync(int id)
    {
        var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.IdCustomer == id);
        return await Task.FromResult(credential);
    }

    /// <summary>
    /// Updates an existing Credential.
    /// </summary>
    /// <param name="updatedCredential">The credential with updated data.</param>
    /// <returns>The updated credential, or null if not found.</returns>
    public async Task<Credential?> UpdateCredentialAsync(Credential updatedCredential)
    {
        var existingCredential = await _context.Credentials
            .FirstOrDefaultAsync(c => c.Id == updatedCredential.Id);

        if (existingCredential == null)
            return null;

        // Updating the credential
        _context.Entry(existingCredential).CurrentValues.SetValues(updatedCredential);

        await _context.SaveChangesAsync();
        return existingCredential;
    }

    public async Task<Credential?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Credentials
            .FirstOrDefaultAsync(c => c.RefreshToken == refreshToken);
    }

}
